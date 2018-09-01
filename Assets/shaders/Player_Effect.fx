float4x4 World;
float4x4 View;
float4x4 Projection;
float4x4 Rotation;
//float4x4 Rotation2;
//bool DrawDepth=true;

//float4x4 ShadowMatrix;

#define MaxLights 1
float maxLights=MaxLights;

float NumbLights=0;
float2 TexMult;
float ShadowDepth=1;
//float4x4 WorldInverseTranspose;
float4 ViewPosition;

//float3 SizeMult;
//int DepthPassNumb;
float Alpha=1;
bool IsFloorEffect=true;
//bool FirstDraw=false;
//bool RePass=false;

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


float4 DiffuseLightPosition;
float Distance;
float4 DiffuseColor;


// output from phong specular will be scaled by this amount
float Shininess=0;

// specular exponent from phong lighting model.  controls the "tightness" of
// specular highlights
float SpecularPower=10;



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
	float2 texCoord: TEXCOORD0;
	float ViewDist:TEXCOORD1;
	float3 WorldPos:TEXCOORD2;
	float4 normal:TEXCOORD3;
	
};

struct VertexShaderInput
{
    float4 Position : POSITION0;
	float4 normal : NORMAL0;
	float2 texCoord            : TEXCOORD0;
    float3 binormal            : BINORMAL0;
    float3 tangent            : TANGENT0;
};

struct VertexShader2Input
{
    float4 Position : POSITION0;
	float2 texCoord            : TEXCOORD0;
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

struct VertexShader2Output
{
    float4 Position : POSITION0;
	float2 texCoord : TEXCOORD0;
	float4 WorldPos: TEXCOORD1;
};

struct BlackVertexShaderInput
{
    float4 Position : POSITION0;
};

struct BlackVertexShaderOutput
{
    float4 Position : POSITION0;
};


VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
//output
    VertexShaderOutput output;

//position
    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
	//float4 shadowPosition= mul(worldPosition,ShadowMatrix);
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

BlackVertexShaderOutput BlackVertexShaderFunction(BlackVertexShaderInput input)
{
//output
    BlackVertexShaderOutput output;
    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);
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
	output.normal=input.normal;
	output.texCoord=input.texCoord;
    return output;
}


VertexShader2Output VertexShader2Function(VertexShader2Input input)
{
//output
    VertexShader2Output output;

//position
    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);
	output.WorldPos=worldPosition;

	
//texCoord
	output.texCoord=input.texCoord;

//return
    return output;
}




float4 PixelShaderFunction2(VertexShaderOutput input) : COLOR0
{


	
 float4 Color=(0,0,0,0);

 //texcoord
    float2 TexCoord=input.texCoord;

  //texture
	float4 Texture=tex2D(DiffuseTextureSampler, TexCoord);

//shadows
 //float4 ShadowBase= texCUBE(ShadowSampler, -normalize(DiffuseLightPosition-input.WorldPos));
 //if(distance(DiffuseLightPosition,input.WorldPos)/(Distance)<((ShadowBase.x+ShadowBase.y+ShadowBase.z)/3)*1.045)//+0.01)
 {



//normal map
  	float3 normalFromMap = (tex2D(NormalMapSampler,TexCoord));
    normalFromMap = mul(normalFromMap, input.tangentToWorld);
    normalFromMap = normalize(normalFromMap);

//diffuse
	float DistMult =(Distance-(distance(DiffuseLightPosition,input.WorldPos)))/Distance;
	float3 LightDirection=normalize(DiffuseLightPosition-input.WorldPos);
	float lightIntensity = max(dot(normalFromMap,  LightDirection),0);

//specular
	float3 reflectedLight = reflect( LightDirection,normalFromMap);
	float spec=dot(reflectedLight,input.View);



//color
	Color +=saturate( DiffuseColor * ((lightIntensity*Texture+ lightIntensity *pow(spec,SpecularPower)*Shininess)*DistMult));

 }

//set alpha
	 Color.w=1;
	 Color*=Alpha*Texture.w;

//return
	return saturate(Color);// +float4(0,0,0,1));
	//return(float4(0.1,0.1,0.1,1));
}




float4 PixelShaderFunction3(VertexShader2Output input) : COLOR0
{

 //texcoord
    float2 TexCoord=input.texCoord;

//texture
	float4 Texture=tex2D(DiffuseTextureSampler, TexCoord);
	
 float4 Color=(0,0,0,0);

 //diffuse
	float DistMult =(Distance-(distance(DiffuseLightPosition,input.WorldPos)))/Distance;
	if(DistMult>0)
	{



//color
	Color +=saturate( DiffuseColor * Texture*DistMult*DistMult*DistMult*DistMult*1.5);

	}
 

//set alpha
		 Color.w=1;
	 Color*=Alpha*Texture.w;

//return
if(Texture.w>0.5)
	return saturate(Color);
	else
	return float4(0,0,0,0);
	//return(float4(0.1,0.1,0.1,1));
}




float4 ShadowPixelShaderFunction(DepthVertexShaderOutput input) : COLOR0
{



float ViewDist2=distance(input.WorldPos,ViewPosition);
  float4 ShadowBase= texCUBE(ShadowSampler, -normalize(DiffuseLightPosition-input.WorldPos));
  if(ViewDist2<(ShadowBase.x+ShadowBase.y+ShadowBase.z)/3*Distance)
 return float4(input.ViewDist/ (ShadowDepth/3)*Alpha,(input.ViewDist /(ShadowDepth/3)-1)*Alpha,(input.ViewDist/  (ShadowDepth/3)-2)*Alpha,Alpha);
  else
   return ShadowBase;

}


float4 FirstShadowPixelShaderFunction(DepthVertexShaderOutput input) : COLOR0
{
 //texcoord
    float2 TexCoord=input.texCoord;

  //texture
	float4 Texture=tex2D(DiffuseTextureSampler, TexCoord);
	if(Texture.w>0.5)
	{
  float  ViewDist2=distance(input.WorldPos,ViewPosition);
   return float4(ViewDist2/ (ShadowDepth/3),ViewDist2 /(ShadowDepth/3)-1,ViewDist2/  (ShadowDepth/3)-2,1);
   }
   else
   return(0,0,0,0);
}


float4 BlackPixelShaderFunction(DepthVertexShaderOutput input) : COLOR0
{
return(0,0,0,0);
}

technique Black
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 BlackVertexShaderFunction();
        PixelShader = compile ps_2_0 BlackPixelShaderFunction();
    }
}

technique Light
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction2();
		 CullMode = CCW;  
    }
}

technique Light2
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShader2Function();
        PixelShader = compile ps_2_0 PixelShaderFunction3();
		CullMode = CCW; 
    }
}

technique FirstShadow
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 DepthVertexShaderFunction();
        PixelShader = compile ps_2_0 FirstShadowPixelShaderFunction();
		 CullMode = None;  
		 CullMode = CW; 
    }
}


technique Shadow
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 DepthVertexShaderFunction();
        PixelShader = compile ps_2_0 ShadowPixelShaderFunction();
		 //CullMode = None;  
		 CullMode = CW; 
    }
}