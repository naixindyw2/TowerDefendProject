// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Unlit alpha-cutout shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "StarSoul/Transparent/Cutout/Unlit" {
Properties {
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_Color ("Main Color(RGB)", Vector) = (1,1,1,1)
	_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
	_RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
    _RimPower ("Rim Power", Range(0.5,8.0)) = 2.0
}
SubShader {
	Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
	LOD 100

	Lighting Off


	Pass {  
		CGPROGRAM
		
			#pragma multi_compile RIM_LIGHT_ON RIM_LIGHT_OFF 
		
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				
#if defined (RIM_LIGHT_ON)
				float3 normal : NORMAL;
#endif
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
				
#if defined (RIM_LIGHT_ON)
				half3 worldNormal : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				
				UNITY_FOG_COORDS(3)
				#else
				
				UNITY_FOG_COORDS(1)
				#endif
				
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Color;
			float4 _RimColor;
			float _RimPower;
			half _Cutoff;
	  
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				
#if defined (RIM_LIGHT_ON)
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
#endif
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.texcoord)*_Color;
				clip(col.a - _Cutoff);
#if defined (RIM_LIGHT_ON)
				fixed3 worldViewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
				half rim = 1.0 - saturate(dot (worldViewDir, i.worldNormal));
				col.rgb += _RimColor.rgb * pow (rim, _RimPower);
#endif
				UNITY_APPLY_FOG(i.fogCoord, col);
				UNITY_OPAQUE_ALPHA(col.a);
				return col;
			}
		ENDCG
	}
	
	
	// Pass to render object as a shadow caster
	Pass {
		Name "ShadowCaster"
		Tags { "LightMode" = "ShadowCaster" }
		
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_shadowcaster
#include "UnityCG.cginc"

struct v2f { 
	V2F_SHADOW_CASTER;
};

v2f vert( appdata_base v )
{
	v2f o;
	TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
	return o;
}

float4 frag( v2f i ) : SV_Target
{
	SHADOW_CASTER_FRAGMENT(i)
}
ENDCG

	}
}
FallBack "Unlit/Transparent Cutout"
CustomEditor "UnlitMaterialInspector"
}
