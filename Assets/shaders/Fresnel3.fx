float4x4 World;
float4x4 View;
float4x4 Projection;
float4 Color;
float4 ViewPosition;
//float TexMult=1;

// TODO: add effect parameters here.

texture2D Texture;
sampler2D DiffuseTextureSampler = sampler_state
{
    Texture = <Texture>;
    MinFilter = linear;
    MagFilter = linear;
    MipFilter = linear;
};

struct VertexShaderInput
{
    float4 Position : POSITION0;
	float4 Normal : NORMAL0;
	float2 TexCoord : TEXCOORD0;

    // TODO: add input channels such as texture
    // coordinates and vertex colors here.
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
		float2 TexCoord:TEXCOORD0;
	float Color:TEXCOORD1;
	float WorldZ:TEXCOORD2;


    // TODO: add vertex shader outputs such as colors and texture
    // coordinates here. These values will automatically be interpolated
    // over the triangle, and provided as input to your pixel shader.
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

	float4 view = normalize(ViewPosition-worldPosition);

	float Mult= (1-abs(dot(view,input.Normal)))*0.8+max(0,1-worldPosition.y/100)/3;
	output.WorldZ=worldPosition.y;
	
	output.Color=Mult;
	output.TexCoord=input.TexCoord;

    // TODO: add your vertex shader code here.

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    // TODO: add your pixel shader code here.
	float4 Texture=tex2D(DiffuseTextureSampler,input.TexCoord);//*TexMult;
	
	//Texture=float4(1,0,0,1);
	float Mult=floor(((input.Color)*(input.Color)*(1+Texture.x))*10)/10;
	//float Mult=input.Color;
	return saturate((Mult)*Color*3);
	//return Texture;
}

technique Technique1
{
    pass Pass1
    {
        // TODO: set renderstates here.

        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
		CullMode=CCW;
    }
}
