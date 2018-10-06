Shader "StarSoul/Transparent/Cutout/Specular_AlphaTest" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 0)
	_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
	_MainTex ("Base (RGB) TransGloss (A)", 2D) = "white" {}
	_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5

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
	Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
	LOD 300
	//Cull Off
CGPROGRAM
#pragma surface surfAlphaTest SSBlinnPhong
// alphatest:_Cutoff

#pragma multi_compile EMISSION_OFF EMISSION_ON  
#pragma multi_compile RIM_LIGHT_ON RIM_LIGHT_OFF  
#pragma multi_compile SPECULAR_OFF SPECULAR_ON

#include "SS_BlinnPhong.inl"

half _Cutoff;

void surfAlphaTest (Input IN, inout SurfaceOutput o) 
{
	fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
	fixed4 c = tex * _Color;
	clip(c.a - _Cutoff);
	o.Albedo = c.rgb;        // o.Emission = tex.rgb;
	o.Alpha = c.a;
	
	fixed4 mask = tex2D(_IllumMask, IN.uv_MainTex);

	/// emission
#if defined (EMISSION_ON)
	o.Emission += tex.rgb * mask.b *_EmissiveStrengh * _EmissiveColor.rgb;
#endif

#if defined (SPECULAR_ON)
	o.Gloss = mask.r;
	o.Specular = _Shininess*mask.g;
#endif

#if defined (RIM_LIGHT_ON)
	half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
	o.Emission += _RimColor.rgb * pow (rim, _RimPower);
#endif

}

ENDCG
}
Fallback "Transparent/Cutout/VertexLit"
CustomEditor "BlinnPhongMaterialInspector"
} 
