// Pixel shader applies a one dimensional gaussian blur filter.
// This is used twice by the bloom postprocess, first to
// blur horizontally, and then again to blur vertically.

sampler TextureSampler : register(s0);
sampler BaseSampler : register(s1);
bool simple=false;
#define RAY_STEPS 3

int step =1;
float margin=30;


float4 PixelShaderFunction(float2 texCoord : TEXCOORD0) : COLOR0
{
   float shift=0;

	float4 base = tex2D(TextureSampler, texCoord);

	float4 smudge= float4(0,0,0,1);
	for(int i=0;i<RAY_STEPS;i++)
	{
	shift+=1;
	smudge+= tex2D(TextureSampler,texCoord+float2(0.00,shift/600))/shift;
	smudge+= tex2D(TextureSampler,texCoord+float2(shift/800,0))/shift;
	smudge+= tex2D(TextureSampler,texCoord+float2(0.00,-shift/600))/shift;
	smudge+= tex2D(TextureSampler,texCoord+float2(-shift/800,0))/shift;
	}
	float4 combinination =(smudge+base)/3;

	 //combinination=base;
	float4 game = tex2D(BaseSampler, texCoord);
	if(simple)
	 return saturate(game);
	 else
	 return saturate(game*(1+combinination.y)*pow(1-float4(combinination.x,combinination.x,combinination.x,1),8)+float4(0,0,0,1));
}


technique GaussianBlur
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
