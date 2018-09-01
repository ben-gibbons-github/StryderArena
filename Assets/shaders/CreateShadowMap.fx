float4x4 World;
float4x4 View;
float4x4 Projection;
float TestVal;


struct VertexShaderInput
{
    float4 Position : POSITION0;
	float4 Normal : NORMAL0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;


};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
	//float4 worldPosition=input.Position;
    float4 viewPosition = mul(worldPosition, View);

    output.Position = mul(viewPosition, Projection);
	output.Position=float4(TestVal,0,0,0)+float4(input.Position.x,input.Position.y,0,0);
    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{



    return saturate(float4(1,0,0,1));
}

technique Ambient
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}