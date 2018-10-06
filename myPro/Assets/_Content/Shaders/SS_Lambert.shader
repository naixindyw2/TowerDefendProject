Shader "StarSoul/Lambert" {
Properties {  /// hEllohUaN
	_Color ("Main Color(RGB)", Vector) = (1,1,1,1)

	_MainTex ("Base (RGB) RefStrength (A)", 2D) = "white" {} 
		
	/// mask texture
	_IllumMask ("Illumin Mask(spec:R,rim:G;emi:B;speExp:A)", 2D) = "white" {}

	_BumpMap ("Normalmap", 2D) = "bump" {}

	/// emissive
	_EmissiveColor ("Emissive Color", Color) = (1,1,1,1) 
	_EmissiveStrengh ("Emissive Strengh", Range(0.0, 10.0)) = 1.0

	/// Rim
	_RimColor ("Rim Color", Color) = (0.0,0.0,0.0,0.0)
    _RimPower ("Rim Power", Range(0.5,8.0)) = 3.0 
}
SubShader {
	LOD 300
	Tags { "RenderType"="Opaque" }
	//Cull Off
CGPROGRAM
#pragma surface surf Lambert

#pragma multi_compile EMISSION_OFF EMISSION_ON  
#pragma multi_compile RIM_LIGHT_ON RIM_LIGHT_OFF 
#pragma multi_compile NORMAL_OFF NORMAL_ON 

#include "SS_Lambert.inl"


ENDCG
}
	
FallBack "VertexLit"
CustomEditor "LambertMaterialInspector"
} 
