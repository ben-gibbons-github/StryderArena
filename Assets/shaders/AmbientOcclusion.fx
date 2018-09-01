// Pixel shader applies a one dimensional gaussian blur filter.
// This is used twice by the bloom postprocess, first to
// blur horizontally, and then again to blur vertically.

sampler TextureSampler : register(s0)
{
    //Texture = (Texture);
    
    MinFilter = Linear;
    MagFilter = Linear;
    MipFilter = Point;
    
    AddressU = Clamp;
    AddressV = Clamp;
};

sampler TextureSampler2 : register(s1);
sampler TextureSampler3 : register(s2);



#define RAY_STEPS 3

int step =1;
float margin=35;
float shift=1;
#define Length 900


float4 PixelShaderFunction(float2 texCoord : TEXCOORD0) : COLOR0
{

    float a = 0;
	float b=0;
	float4 base = tex2D(TextureSampler, texCoord)+tex2D(TextureSampler2, texCoord)+tex2D(TextureSampler3, texCoord);
	float dist = (base.x+base.y+base.z)*Length;
	float4 newbase;
	float newdist;
	float mult=1;
	float tempmult=0;

	if(dist<3000)
{
	newbase = tex2D(TextureSampler, texCoord+float2(-shift/640,0)*step)+tex2D(TextureSampler2, texCoord+float2(-shift/640,0)*step)+tex2D(TextureSampler3, texCoord+float2(-shift/640,0)*step);
	newdist= (newbase.x+newbase.y+newbase.z)*Length;
	tempmult=clamp(2-(dist-newdist)/margin,0,1);
	a+=(dist-newdist)/200*tempmult;
	mult*=tempmult;
	

	newbase = tex2D(TextureSampler, texCoord+float2(shift/640,0)*step)+tex2D(TextureSampler2, texCoord+float2(shift/640,0)*step)+tex2D(TextureSampler3, texCoord+float2(shift/640,0)*step);
	newdist= (newbase.x+newbase.y+newbase.z)*Length;
	tempmult=clamp(2-(dist-newdist)/margin,0,1);
	a+=(dist-newdist)/200*tempmult;
	mult*=tempmult;

	newbase = tex2D(TextureSampler, texCoord+float2(0,-shift/480)*step)+tex2D(TextureSampler2, texCoord+float2(0,-shift/480)*step)+tex2D(TextureSampler3, texCoord+float2(0,-shift/480)*step);
	newdist= (newbase.x+newbase.y+newbase.z)*Length;
	tempmult=clamp(2-(dist-newdist)/margin,0,1);
	a+=(dist-newdist)/200*tempmult;
	mult*=tempmult;

	newbase = tex2D(TextureSampler, texCoord+float2(0,shift/480)*step)+tex2D(TextureSampler2, texCoord+float2(0,shift/480)*step)+tex2D(TextureSampler3, texCoord+float2(0,shift/480)*step);
	newdist= (newbase.x+newbase.y+newbase.z)*Length;
	tempmult=clamp(2-(dist-newdist)/margin,0,1);
	a+=(dist-newdist)/200*tempmult;
	mult*=tempmult;
	}

	
a-=0.025;

	b=0;
	//a=(a-0.2)*(1/0.9);
	a*=mult;
	//a*=min(0.2,a)*5;
	a=max(0,a)/step;

	a*=(1-dist/(Length*3));
    return saturate(float4(a,b,0,1));
}


technique GaussianBlur
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
