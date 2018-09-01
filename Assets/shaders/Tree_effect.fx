

float4x4 World;
float4x4 View;
float4x4 Projection;
float4x4 Rotation;

#define MaxLights  8
float maxLights=MaxLights;
float NumbLights=1;

float ShadowDepth=1;
bool IsFloorEffect=false;
//float4x4 WorldInverseTranspose;
float4 ViewPosition;


float Alpha=1;


//#define Length 900


texture2D Texture;
sampler2D DiffuseTextureSampler = sampler_state
{
    Texture = <Texture>;
    MinFilter = linear;
    MagFilter = linear;
    MipFilter = linear;
};


texture2D NormalMap;
sampler2D NormalMapSampler = sampler_state
{
    Texture = <NormalMap>;
    MinFilter = linear;
    MagFilter = linear;
    MipFilter = linear;
};


float4 DiffuseLightPosition[MaxLights];
float Distance[MaxLights];
float4 DiffuseColor[MaxLights];
//float4 DiffuseDirection[MaxLights];
//bool DiffuseIsSpot[MaxLights];

// output from phong specular will be scaled by this amount
float Shininess=100;

// specular exponent from phong lighting model.  controls the "tightness" of
// specular highlights
float SpecularPower=10;




struct DepthVertexShaderInput
{
    float4 Position : POSITION0;
	float4 normal : NORMAL0;
	float2 texCoord            : TEXCOORD0;
};

struct DepthVertexShaderOutput
{
    float4 Position : POSITION0;
	float ViewDist:TEXCOORD0;
	float3 WorldPos:TEXCOORD1;
};

struct VertexShaderInput
{
    float4 Position : POSITION0;
	float4 normal : NORMAL0;
	float2 texCoord            : TEXCOORD0;
	float3 binormal            : BINORMAL0;
    float3 tangent            : TANGENT0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float2 texCoord : TEXCOORD0;
    float4 Color: TEXCOORD1;
	float3x3 tangentToWorld : TEXCOORD2;
	float3 View:TEXCOORD5;
	float3 BestDirection:TEXCOORD6;
};


VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
//output
    VertexShaderOutput output;

//position
    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

// view
	float3 View=normalize(worldPosition-ViewPosition);

//texCoord
	output.texCoord=input.texCoord;
	
//define color
	float4 Color=(0,0,0,0);
	float4 Spec=(0,0,0,0);
	float3 BestDirection =float3(0,0,0);
	float Divider=0;

	for(int i=0;i<max(1,NumbLights);i++)
	{

//diffuse
	float DistMult =max(0,(Distance[i]-(distance(DiffuseLightPosition[i],worldPosition)))/Distance[i]);
	float3 DiffuseDirection= normalize(DiffuseLightPosition[i]-worldPosition);

	BestDirection+=DiffuseDirection*DistMult;
	Divider+=DistMult;
	

//color
	Color =saturate(Color+ DiffuseColor[i] *DistMult);

	}

//best direction
    output.BestDirection=BestDirection/Divider;

//normal map
	output.tangentToWorld[0] = mul(input.tangent,Rotation);
    output.tangentToWorld[1] = mul(input.binormal,Rotation);
    output.tangentToWorld[2] = mul(input.normal,Rotation);

	
// view
	output.View=normalize(worldPosition-ViewPosition);


	 output.Color=Color;


//return
    return output;
}


DepthVertexShaderOutput DepthVertexShaderFunction(DepthVertexShaderInput input)
{

    DepthVertexShaderOutput output;
    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);
    output.ViewDist=distance(worldPosition,ViewPosition);
	output.WorldPos=worldPosition;
    return output;
}






float4 PixelShaderFunction2(VertexShaderOutput input) : COLOR0
{

//normal map
  	float3 normalFromMap = (tex2D(NormalMapSampler,input.texCoord));
    normalFromMap = mul(normalFromMap, input.tangentToWorld);
    normalFromMap = normalize(normalFromMap);

//diffuse
	float Mult= dot(normalFromMap,input.BestDirection);

//specular
	float3 reflectedLight = reflect(-input.BestDirection,normalFromMap);
	float spec=dot(reflectedLight,input.View);

//texture
	float4 Texture=tex2D(DiffuseTextureSampler, input.texCoord);
	
//return
    
	//float4 R =saturate(Texture*input.Color*(Mult+pow(spec,SpecularPower)*Shininess));
	float4 R= saturate(Texture*input.Color*Mult+pow(spec,SpecularPower)*Shininess*input.Color);
	R.w=Alpha;

	return R;
	//return(float4(0.1,0.1,0.1,1));
}


float4 ShadowPixelShaderFunction(DepthVertexShaderOutput input) : COLOR0
{
   float ViewDist2=distance(input.WorldPos,ViewPosition);
   return float4(ViewDist2/ (ShadowDepth/3),ViewDist2 /(ShadowDepth/3)-1,ViewDist2/  (ShadowDepth/3)-2,1);
}

technique Light
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction2();
		CullMode=CCW;
    }
}




technique Shadow
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 DepthVertexShaderFunction();
        PixelShader = compile ps_2_0 ShadowPixelShaderFunction();
		 CullMode = None;  
    }
}