Shader "Custom/CartoonRender" {
    Properties {
        _MainTex("Main Texture", 2D) = "white" {}
        _ShadowTex("Shadow Texture", 2D) = "white" {}
        _Outline("Outline", Range(0, 1)) = 0.005
        _OutlineColor("Outline Color", Color) = (0, 0, 0, 1)
        _RimColor("Rim Color", Color) = (0, 0, 0, 0)
        _RimPower("Rim Power", Range(0.01, 10.0)) = 3.0
        _RimRange("Rim Range", Range(0, 1)) = 0.5
		_TintColor("Tint Color", Color) = (0, 0, 0, 0)
    }

    SubShader {
        Tags { "RenderType"="Opaque" "Queue"="Geometry"}

        // Outline Pass
        Pass {
            // Options
            Cull Front

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            // Properties
            float _Outline;
            fixed4 _OutlineColor;

            struct a2v {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f {
                float4 pos : SV_POSITION;
            };

            v2f vert(a2v v)
            {
                v2f o;

                float4 pos = mul(UNITY_MATRIX_MV, v.vertex);
                float3 normal = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
                normal.z = -0.5;
                pos = pos + float4(normalize(normal), 0) * _Outline;
                o.pos = mul(UNITY_MATRIX_P, pos);

                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                return float4(_OutlineColor.rgb, 1);
            }

            ENDCG
        }

        // Main Pass
        Pass {
            Tags { "LightMode"="ForwardBase" }

            // Options
            Cull back

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"

            // Properties
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _ShadowTex;
            float4 _ShadowTex_ST;
            fixed4 _RimColor;
            float _RimPower;
            float _RimRange;
			fixed4 _TintColor;

            struct a2v {
                float4 vertex : POSITION;
                float4 texcoordMain : TEXCOORD0;
                float4 texcoordShadow  : TEXCOORD1;
                float3 normal : NORMAL;
            };

            struct v2f {
                float4 pos : POSITION;
                float2 uvMain : TEXCOORD0;
                float2 uvShadow : TEXCOORD1;
                float3 worldNormal : TEXCOORD2;
                float3 worldPos : TEXCOORD3;
            };

            v2f vert(a2v v)
            {
                v2f o;

                o.pos = UnityObjectToClipPos(v.vertex);
                o.uvMain = TRANSFORM_TEX(v.texcoordMain, _MainTex);
                o.uvShadow = TRANSFORM_TEX(v.texcoordShadow, _ShadowTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 mainColor = tex2D(_MainTex, i.uvMain);
                fixed4 shadowColor = tex2D(_ShadowTex, i.uvShadow);

                float3 worldNormal = normalize(i.worldNormal);
                float3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
                float3 worldViewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));

                float rim = 1.0 - saturate(dot(worldViewDir, worldNormal));
                rim = step(1.0 - _RimRange, rim) * rim;
                fixed4 rimColor = _RimColor * pow(rim, _RimPower);

                float diff = dot(worldNormal, worldLightDir);
                diff = diff * 0.5 + 0.5;
                if (diff > 0.5) {
                    return mainColor + _TintColor;
                } else {
                    return (shadowColor + rimColor) + _TintColor;
                }
            }

            ENDCG
        }
    }

    FallBack "Diffuse"
}
