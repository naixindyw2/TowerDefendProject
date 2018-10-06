
sampler2D _MainTex;
sampler2D _BumpMap;
sampler2D _IllumMask;

float4 _Color;
float4 _EmissiveColor;
float _EmissiveStrengh;

float4 _RimColor;
float _RimPower;
half _Shininess;
	  
struct Input {
	float2 uv_MainTex;
	float2 uv_BumpMap;
	float3 viewDir;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
	fixed4 c = tex * _Color;
	
	o.Albedo = c.rgb;        // o.Emission = tex.rgb;
	o.Alpha = c.a;
	
	fixed4 mask = tex2D(_IllumMask, IN.uv_MainTex);

	/// emission
#if defined (EMISSION_ON)
	o.Emission += tex.rgb * mask.b *_EmissiveStrengh * _EmissiveColor.rgb;
#endif
	
#if defined (NORMAL_ON)
	o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
#endif

#if defined (RIM_LIGHT_ON)
	half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
	o.Emission += _RimColor.rgb * pow (rim, _RimPower);
#endif

}
