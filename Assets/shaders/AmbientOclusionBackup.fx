// Pixel shader applies a one dimensional gaussian blur filter.
// This is used twice by the bloom postprocess, first to
// blur horizontally, and then again to blur vertically.

sampler TextureSampler : register(s0);

#define RAY_STEPS 5



float4 PixelShaderFunction(float2 texCoord : TEXCOORD0) : COLOR0
{
    float a = 0.5;
	float shift=0.01;
	float4 base = tex2D(TextureSampler, texCoord);
	
	float dist = (base.x+base.y+base.z)*1000;
	

	float2 move;
	float4 newbase;
	float newdist;
	float temp;
	float passamount=400;
	float repeat;

	for(int l=0;l<4;l++)
	{

	if(l=0)
	move= float2(0.003,0);
		if(l=1)
	move= float2(0,0.003);
		if(l=2)
	move= float2(-0.003,0);
		if(l=3)
	move= float2(0,-0.003);
    for (int i = 0; i < RAY_STEPS; i++)
	{
       newbase = tex2D(TextureSampler, texCoord + i*move);
	   newdist=(newbase.x+newbase.y+newbase.z)*1000;
	   repeat=dist-newdist;
	   temp= repeat -(repeat*min(1,max(0,1-(abs(repeat/passamount)-0.5))));
	   //if(abs((-temp/100))>0.2)
	   a-=(temp/100);
    }
	}



	

    return saturate(float4(a,a,a,1));
}


technique GaussianBlur
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
