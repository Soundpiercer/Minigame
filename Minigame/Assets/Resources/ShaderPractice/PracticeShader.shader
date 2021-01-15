// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
// sesisoft : seok jun hyuk - tweaked Legacy/Diffuse-Normal
Shader "Custom/PracticeShader" 
{
	Properties 
	{
		// 레거시 Diffuse 셰이더 코드
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}

		// sesisoft : seok jun hyuk - 외곽선
        _Outline ("Outline", Float) = 0.1
		_OutlineColor("Outline Color", Color) = (1,1,1,1)   

		// sesisoft : seok jun hyuk - ShaderLab연습
		//_SpecColor ("Spec Color", Color) = (1,1,1,1)
        //_Emission ("Emmisive Color", Color) = (0,0,0,0)
        //_Shininess ("Shininess", Range (0.01, 1)) = 0.7      
	}
	SubShader
	{
		// 레거시 Diffuse 셰이더 코드
		/*
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM

			#pragma surface surf Lambert

			sampler2D _MainTex;
			fixed4 _Color;

			struct Input 
			{
				float2 uv_MainTex;
			};

			void surf (Input IN, inout SurfaceOutput o) 
			{
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			    o.Albedo = c.rgb;
			    o.Alpha = c.a;
			}

		ENDCG
		*/
		// 외곽선 그리기
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Front // 뒷면만 그리기
			ZWrite Off

			CGPROGRAM

				#pragma vertex drawVertex // vert 는 버텍스 셰이더
				#pragma fragment drawFragment // frag 은 프래그먼트 셰이더
				#include "UnityCG.cginc"

				half _Outline; // float의 절반사이즈(16bit) 부동소수점
				half4 _OutlineColor; // float의 절반사이즈(16bit) 부동소수점의 1차원 배열

				struct vertexInput
				{
					float4 vertex: POSITION;
				};

				struct vertexOutput
				{
					float4 pos: SV_POSITION;
					float2 uvs: TEXCOORD0;
				};

				float4 drawVertex(float4 position : POSITION, float3 normal : NORMAL) : SV_POSITION
				{
					float4 clippedPosition = UnityObjectToClipPos(position);
					float3 clippedNormal = mul((float3x3) UNITY_MATRIX_VP, mul((float3x3) UNITY_MATRIX_M, normal));

					clippedPosition.xyz += clippedNormal * _Outline;
					return clippedPosition; //UnityObjectToClipPos(position);
				}

				half4 drawFragment(vertexOutput i) : COLOR
				{
					return _OutlineColor;
				}

			ENDCG
		}
		// ShaderLab 연습용
		/*
		Pass
		{
			Material 
			{
				Diffuse [_Color]
				Ambient [_Color]
                Specular [_SpecColor]
                Shininess [_Shininess]
                Emission [_Emission]
			}
		}
		*/
	}

	Fallback "Legacy Shaders/VertexLit"
}