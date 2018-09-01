

sampler TextureSampler : register(s0);

float Alpha;
float Angle;


float4 PixelShaderFunction(float2 texCoord : TEXCOORD0) : COLOR0
{
  
	
	if(atan2(texCoord.x-0.5,texCoord.y-0.5)>Angle)
	{
	float4 base = saturate( tex2D(TextureSampler, texCoord)*Alpha);
	//base.w=Alpha*base.w;
	return base;
	}
	else
	return float4(0,0,0,0);
}


technique GaussianBlur
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
