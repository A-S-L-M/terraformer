Shader "Shader/Earth Deformation"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
			// apply fog
			UNITY_APPLY_FOG(i.fogCoord, col);
			return col;
		}
		ENDCG
	}
	}
}





/*
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Shader/Earth Deformation"
{
	Properties
	{
		_Color("", Color) = (1,1,1,1)
		_MainTex("Albedo(RGB)", 2D) = "white"{}
	_Glossiness("Smoothness", Range(0,1)) =0.5
		_Metallic("Metallic", Range(0,1)) =0.0
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


			v.vertex.xyz = worldPos;//normal * squish;
			//v.normal = normalize(worldPos.x, worldPos.y, worldPos.z);
		}

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

			o.Albedo = c.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
		}

		ENDCG
	}
		FallBack "Diffuse"
}
*/