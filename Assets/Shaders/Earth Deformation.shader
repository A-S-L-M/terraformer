// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Shader/Earth Deformation"
{
	Properties
	{
		_Color("", Color) = (1,1,1,1)
		_MainTex("Albedo(RGB)", 2D) = "white"{}
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }

		CGPROGRAM

		#pragma surface surf Standard vertex:vert nolightmap addshadow
		#pragma target 3.0

		#include "UnityCG.cginc"

		struct Input { float2 uv_MainTex; };

	sampler2D _MainTex;
		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void vert(inout appdata_full v)
		{
			float3 worldPos = UnityObjectToClipPos(v.vertex);

			float squish = 1; // -min(worldPos.y, _SquishLimit) * _SquishAmount;

			float3 normal = UnityObjectToClipPos(v.normal);


			v.vertex.xyz = normal * squish;
			//v.normal = normalize(cross(v2 - v1, v3 - v1));
		}

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

			o.Albedo = _Color.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
		}

		ENDCG
	}
		FallBack "Diffuse"
}