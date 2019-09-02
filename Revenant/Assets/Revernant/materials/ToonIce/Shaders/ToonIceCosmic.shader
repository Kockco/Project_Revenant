
Shader "Toon/IceCosmic" {

	Properties{
		_Color("Main Color", Color) = (0.49,0.94,0.64,1)
		_TColor("Top Color", Color) = (0.49,0.94,0.64,1)
		_BottomColor("Bottom Color", Color) = (0.23,0,0.95,1)
		_Ramp("Toon Ramp (RGB)", 2D) = "gray" {}
		_BumpMap("Normal", 2D) = "bump" {}

		_RimBrightness("Rim Brightness", Range(3,4)) = 3.2
		[Toggle(ALPHA)] _ALPHA("Enable Alpha?", Float) = 0
		_Offset("Gradient Offset", Range(-4,4)) = 3.2


	}

		SubShader{
		Tags{ "RenderType" = "Opaque" }
		Cull Back


		Blend SrcAlpha OneMinusSrcAlpha
			//Blend One One
		//	Blend One OneMinusSrcAlpha
			LOD 200
			CGPROGRAM
	#pragma surface surf ToonRamp keepalpha
	#pragma shader_feature ALPHA

			sampler2D _Ramp;
			sampler2D _BumpMap;




	float _Offset;
	float4 _Color;
	float4 _TColor;
	float4 _BottomColor;
	float _RimBrightness;


	struct Input {
		float3 viewDir;
		float3 worldPos;
		float2 uv_BumpMap;

	};

	void surf(Input IN, inout SurfaceOutput o) {
		o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
		float3 localPos = (IN.worldPos - mul(unity_ObjectToWorld, float4(0, 0, 0, 1)).xyz);// local position of the object, with an offset, clamped to make sure it doesn't go into negative
		float3 AdjustLocalPos = saturate(float3(localPos.x, localPos.y, localPos.z)) + 0.4;
		float softRim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));// calculate a soft fresnel based on the view direction and the normals of the object
		float hardRim = round(softRim); // round it up for a harder edge
		o.Emission = _Color * lerp(hardRim, softRim, saturate(AdjustLocalPos.x + AdjustLocalPos.y))  * lerp(0,_RimBrightness,AdjustLocalPos.y);	 // lerp the emission from the hard rim to the softer one, based on the position
		float innerRim = 1.5 + saturate(dot(normalize(IN.viewDir), o.Normal));




		o.Albedo = _Color * pow(innerRim, 0.7)*lerp(_BottomColor, _TColor, saturate(localPos.y + _Offset));
		o.Alpha = 1;
#if ALPHA
		o.Alpha = 1 * softRim * (2 - saturate(localPos.y));

#endif
	}

#pragma lighting ToonRamp 
	inline half4 LightingToonRamp(SurfaceOutput s, half3 lightDir, half atten)
	{
#ifndef USING_DIRECTIONAL_LIGHT
		lightDir = normalize(lightDir);
#endif

		half d = dot(s.Normal, lightDir)*0.5 + 0.5;
		half3 ramp = tex2D(_Ramp, float2(d, d)).rgb;

		half4 c;

		c.rgb = s.Albedo * _LightColor0.rgb * ramp * (atten * 2);
		c.a = s.Alpha;

		return c;
	}
	ENDCG

		}

			Fallback "Diffuse"
}
