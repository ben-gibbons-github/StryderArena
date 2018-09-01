float4x4 World;
float4x4 View;
float4x4 Projection;
float4x4 Rotation;
float4x4 Rotation2;
bool DrawDepth=true;

float4x4 ShadowMatrix;

#define MaxLights 2

float NumbLights=0;
float2 TexMult;
float ShadowDepth=1;
float4x4 WorldInverseTranspose;
float4 ViewPosition;
float3 SizeMult;
int DepthPassNumb;
float Alpha=1;
bool FirstDraw=false;
bool RePass=false;

#define Length 900

Texture ShadowCube; 
samplerCUBE ShadowSampler = sampler_state 
{ 
   texture = <ShadowCube> ; 
   magfilter = LINEAR; 
   minfilter = LINEAR; 
   mipfilter = LINEAR; 
   AddressU = Mirror; 
   AddressV = Mirror; 
}; 

Texture ShadowPlane; 
sampler2D ShadowPlaneSampler = sampler_state 
{ 
    Texture = <ShadowPlane>;
    MinFilter = linear;
    MagFilter = linear;
    MipFilter = linear;
}; 


texture2D LumMap;
sampler2D LumMapSampler = sampler_state
{
    Texture = <LumMap>;
    MinFilter = linear;
    MagFilter = linear;
    MipFilter = linear;
};


texture2D SpecMap;
sampler2D SpecMapSampler = sampler_state
{
    Texture = <SpecMap>;
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

texture2D Texture;
sampler2D DiffuseTextureSampler = sampler_state
{
    Texture = <Texture>;
    MinFilter = linear;
    MagFilter = linear;
    MipFilter = linear;
};


float4 DiffuseLightPosition[MaxLights]={float4(0,0,0,0),float4(0,0,0,0)};
float Distance[MaxLights]={float(1000),float(1000)};
float4 DiffuseColor[MaxLights]={float4(1,0,0,1),float4(0,0,1,1)};
float4 DiffuseDirection[MaxLights]={float4(0.5,0.9,0.5,0),float4(0.5,0.5,0.5,0)};
bool DiffuseIsSpot[MaxLights]={true,true};

// output from phong specular will be scaled by this amount
float Shininess=0;

// specular exponent from phong lighting model.  controls the "tightness" of
// specular highlights
float SpecularPower=10;

float4 AmbientColor = float4(1, 0.5, 0.25, 0);
float AmbientIntensity = 0.1;


struct DepthVertexShaderInput
{
    float4 Position : POSITION0;
	float4 normal : NORMAL0;
	float2 texCoord            : TEXCOORD0;
    float3 binormal            : BINORMAL0;
    float3 tangent            : TANGENT0;
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
	float4 WorldPos: TEXCOORD1;
	float3x3 tangentToWorld : TEXCOORD2;
	float3 View:TEXCOORD5;
	float ViewDist:TEXCOORD6;
};


VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
//output
    VertexShaderOutput output;

//position
    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
	float4 shadowPosition= mul(worldPosition,ShadowMatrix);
    output.Position = mul(viewPosition, Projection);
	output.WorldPos=worldPosition;

// view
	output.View=normalize(worldPosition-ViewPosition);
	
//texCoord
	output.texCoord=input.texCoord;

//normal map
	output.tangentToWorld[0] = mul(input.tangent,Rotation);
    output.tangentToWorld[1] = mul(input.binormal,Rotation);
    output.tangentToWorld[2] = mul(input.normal,Rotation);
	
//viewdistance
	output.ViewDist=distance(worldPosition,ViewPosition);

//return
    return output;
}

DepthVertexShaderOutput DepthVertexShaderFunction(DepthVertexShaderInput input)
{
//output
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


	
 float4 Color=(0,0,0,0);
 Color.w=1;
  
  //int i=0;
  float4 ShadowBase= texCUBE(ShadowSampler, -normalize(DiffuseLightPosition[0]-input.WorldPos));
  if(distance(DiffuseLightPosition[0],input.WorldPos)/(Distance[0])<((ShadowBase.x+ShadowBase.y+ShadowBase.z)/3)*1.045)//+0.01)
  {
  float2 TexCoord=float2(clamp(input.texCoord.x*TexMult.x-(floor(input.texCoord.x*TexMult.x)),0,1),clamp(input.texCoord.y*TexMult.y-(floor(input.texCoord.y*TexMult.y)),0,1));
  	    float3 normalFromMap = (tex2D(NormalMapSampler,TexCoord)-float4(0.5,0.5,0.5,0))+float4(0.5,0.5,0.5,0);
		//normalFromMap=float3(0,0,0);
    normalFromMap = mul(normalFromMap, input.tangentToWorld);
    normalFromMap = normalize(normalFromMap);
	float DistMult =(Distance[0]-(distance(DiffuseLightPosition[0],input.WorldPos)))/Distance[0];
	float3 LightDirection=normalize(DiffuseLightPosition[0]-input.WorldPos);
	float lightIntensity = max(dot(normalFromMap,  LightDirection),0);
		float3 reflectedLight = reflect( LightDirection,normalFromMap);
	float spec=dot(reflectedLight,input.View);
	float4 Texture=tex2D(DiffuseTextureSampler, TexCoord);
	Color +=saturate( DiffuseColor[0] * ((lightIntensity*Texture+ lightIntensity *pow(spec,SpecularPower)*Shininess)*DistMult));
	Color.w=Alpha;
	}

	
	
	return saturate(Color);// +float4(0,0,0,1));

}


float4 DepthPixelShaderFunction(DepthVertexShaderOutput input) : COLOR0
{
   return float4(clamp(input.ViewDist/Length-DepthPassNumb,0,1),clamp(input.ViewDist/Length-1-DepthPassNumb,0,1),clamp(input.ViewDist/Length-2-DepthPassNumb,0,1),1);
}

float4 ShadowPixelShaderFunction(DepthVertexShaderOutput input) : COLOR0
{
if(RePass)
{
  float4 ShadowBase= texCUBE(ShadowSampler, -normalize(DiffuseLightPosition[0]-input.WorldPos));
  if(ShadowDepth-100<(ShadowBase.x+ShadowBase.y+ShadowBase.z)/3*Distance[0])
 return float4(input.ViewDist/ (ShadowDepth/3),input.ViewDist /(ShadowDepth/3)-1,input.ViewDist/  (ShadowDepth/3)-2,1);
  else
   return ShadowBase;
   }
   else
   {
   float ViewDist2=distance(input.WorldPos,ViewPosition);
   return float4(ViewDist2/ (ShadowDepth/3),ViewDist2 /(ShadowDepth/3)-1,ViewDist2/  (ShadowDepth/3)-2,1);
  }
}

technique Light
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction2();
    }
}

technique Depth
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 DepthVertexShaderFunction();
        PixelShader = compile ps_2_0 DepthPixelShaderFunction();
    }
}

technique Shadow
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 DepthVertexShaderFunction();
        PixelShader = compile ps_2_0 ShadowPixelShaderFunction();
		 //CullMode = None;  
    }
}