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
float Shininess=7.5;

// specular exponent from phong lighting model.  controls the "tightness" of
// specular highlights
float SpecularPower=25;

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
	float2 ShadowPos:TEXCOORD7;
};


VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;
    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
	float4 shadowPosition= mul(worldPosition,ShadowMatrix);

		output.ShadowPos=float2(0.5*shadowPosition.xy/shadowPosition.w+float2(0.5,0.5));
	output.ShadowPos.y=1-output.ShadowPos.y;

    output.Position = mul(viewPosition, Projection);
	output.View=normalize(worldPosition-ViewPosition);
	//output.texCoord = input.texCoord;
	float4 setup = mul(input.normal,Rotation2);
	setup=input.normal;
	//output.texCoord= float2(input.texCoord.x*(lerp(0,1,(worldPosition.x-400)/-800)+(input.Position.z-400)/-800),input.texCoord.y*(worldPosition.y-400)/-800);
	output.texCoord= float2(input.texCoord.x*(lerp(0,100,abs(worldPosition.x))/100+lerp(0,100,abs(worldPosition.x))/100),input.texCoord.y*lerp(0,100,abs(worldPosition.x))/100);
	//output.texCoord.x+=input.texCoord.y;
	//output.texCoord.y=input.texCoord.y;
	output.texCoord=input.texCoord;
	output.tangentToWorld[0] = mul(input.tangent,Rotation);
    output.tangentToWorld[1] = mul(input.binormal,Rotation);
    output.tangentToWorld[2] = mul(input.normal,Rotation);
	output.WorldPos=worldPosition;
	output.ViewDist=distance(worldPosition,ViewPosition);
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

    float3 normalFromMap = tex2D(NormalMapSampler, input.texCoord);
    normalFromMap = mul(normalFromMap, input.tangentToWorld);
    normalFromMap = normalize(normalFromMap);
	
 float4 Color=(0,0,0,0);
 
  
	for(int i=0;i<1;i++)
	{
	float DistMult =max((Distance[i]-(distance(DiffuseLightPosition[i],input.WorldPos)))/Distance[i],0);

	float4 ShadowBase= texCUBE(ShadowSampler, -normalize(input.WorldPos-DiffuseLightPosition[i]));
	if(distance(DiffuseLightPosition[i],input.WorldPos)/Distance[i]<(ShadowBase.x+ShadowBase.y+ShadowBase.z)/3+0.01)
	{

	float3 LightDirection=normalize(DiffuseLightPosition[i]-input.WorldPos);
	float lightIntensity = max(dot(normalFromMap,  LightDirection),0);
	Color +=saturate( DiffuseColor[i] * lightIntensity *tex2D(DiffuseTextureSampler, input.texCoord) * DistMult);
	}
	
	}
	//Color+=+tex2D(LumMapSampler,input.texCoord);
	
    return saturate((Color) +float4(0,0,0,1));
	//float dist = distance(input.WorldPos,ViewPosition)/4000;
	//
	
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{


	
 float4 Color=(0,0,0,0);
 
  
  int i=0;
	float4 ShadowBase;
	float mult=1;
	if(!DiffuseIsSpot[i])
	mult=2.5;
	float DistMult =min(max((Distance[i]-(distance(DiffuseLightPosition[i],input.WorldPos)))/Distance[i],0)*mult,1);
	if(DistMult>0)
	{
	float2 TexCoord=float2(input.texCoord.x*TexMult.x-(floor(input.texCoord.x*TexMult.x)),input.texCoord.y*TexMult.y-(floor(input.texCoord.y*TexMult.y)));
	    float3 normalFromMap = tex2D(NormalMapSampler,TexCoord);
    normalFromMap = mul(normalFromMap, input.tangentToWorld);
    normalFromMap = normalize(normalFromMap);
	
	//TexCoord=input.texCoord*10;
	if(DiffuseIsSpot[i])
	ShadowBase= texCUBE(ShadowSampler, -normalize(DiffuseLightPosition[i]-input.WorldPos));
	else
	{
	float4 shadowPositionn= mul(input.WorldPos,ShadowMatrix);
float2 ShadowPoss=float2(0.5*shadowPositionn.xy/shadowPositionn.w+float2(0.5,0.5));
	ShadowPoss.y=1-ShadowPoss.y;
	ShadowBase= tex2D(ShadowPlaneSampler,ShadowPoss);
	//ShadowBase=float4(1,1,1,1);
	}
	if(distance(DiffuseLightPosition[i],input.WorldPos)/Distance[i]<(ShadowBase.x+ShadowBase.y+ShadowBase.z)/3+0.01)
	{
	float3 LightDirection=normalize(DiffuseLightPosition[i]-input.WorldPos);
	float DireMult=1;
	if(!DiffuseIsSpot[i])
	DireMult=min((dot(LightDirection,DiffuseDirection[i])-0.95)*32,1);
	float lightIntensity = max(dot(normalFromMap,  LightDirection),0);
	float3 reflectedLight = reflect( LightDirection,normalFromMap);
	float spec=dot(reflectedLight,input.View);
	Color +=saturate( DiffuseColor[i] * (lightIntensity 
	*tex2D(DiffuseTextureSampler, TexCoord)
	+ lightIntensity *pow(spec,SpecularPower)*Shininess*tex2D(SpecMapSampler, TexCoord)) * DistMult*DireMult);
	
	}
	
	}
	return saturate(Color +float4(0,0,0,1));

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
  if(ShadowDepth-1<(ShadowBase.x+ShadowBase.y+ShadowBase.z)/3*Distance[0])
 return float4(input.ViewDist/ (ShadowDepth/3),input.ViewDist /(ShadowDepth/3)-1,input.ViewDist/  (ShadowDepth/3)-2,1);
  else
   return ShadowBase;
   }
   else
   return float4(input.ViewDist/ (ShadowDepth/3),input.ViewDist /(ShadowDepth/3)-1,input.ViewDist/  (ShadowDepth/3)-2,1);
  
}

technique Light
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}

technique Light_NoSpec
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
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
		 CullMode = None;  
    }
}