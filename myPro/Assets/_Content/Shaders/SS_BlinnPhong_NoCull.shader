Shader "StarSoul/BlinnPhong_NoCull" {
Properties {  /// hEllohUaN
	_Color ("Main Color(RGB)", Vector) = (1,1,1,1)
	_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
	_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
	_MainTex ("Base (RGB) RefStrength (A)", 2D) = "white" {} 
	/// mask texture
	_IllumMask ("Illumin Mask(spec:R;speExp:G;emi:B)", 2D) = "white" {}

	/// emissive
	_EmissiveColor ("Emissive Color", Color) = (1,1,1,1) 
	_EmissiveStrengh ("Emissive Strengh", Range(0.0, 10.0)) = 1.0

	/// Rim
	_RimColor ("Rim Color", Color) = (0.0,0.0,0.0,0.0)
    _RimPower ("Rim Power", Range(0.5,8.0)) = 3.0 
}
SubShader {
	LOD 400
	Tags { "RenderType"="Opaque" }
	Cull Off
CGPROGRAM
#pragma surface surf SSBlinnPhong

#pragma multi_compile EMISSION_OFF EMISSION_ON  
#pragma multi_compile RIM_LIGHT_ON RIM_LIGHT_OFF  
#pragma multi_compile SPECULAR_OFF SPECULAR_ON

#include "SS_BlinnPhong.inl"

ENDCG
}
	
FallBack "VertexLit"
CustomEditor "BlinnPhongMaterialInspector"
} 
