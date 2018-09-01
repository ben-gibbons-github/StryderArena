// Pixel shader applies a one dimensional gaussian blur filter.
// This is used twice by the bloom postprocess, first to
// blur horizontally, and then again to blur vertically.

sampler TextureSampler : register(s0);

#define RAY_STEPS 5

bool simple=false;

float4 PixelShaderFunction(float2 texCoord : TEXCOORD0) : COLOR0
{
   
	float4 base = tex2D(TextureSampler, texCoord);
	float a;
	if (simple)
	 a = (base.x*4000+base.y*(4000/50))/4000;
	 else
	 a = (base.x*4000)/4000;
	 
	 
    return saturate(float4(a,a,a,1));
	//else
	//return saturate(base);
}


technique GaussianBlur
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
