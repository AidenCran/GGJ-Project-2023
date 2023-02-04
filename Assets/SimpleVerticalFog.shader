Shader "NLS/SimpleVerticalFog"
{
	Properties 
	{
		_Color("Fog Color", Color) = (1, 1, 1, 1)

		_DepthFactor("Depth Factor", Range(0,5)) = 1.0

		_NoiseSize("Noise Displacement", Float) = 10.
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent"  }
		LOD 100
		Pass
		{
			ZWrite Off

			//Regular alpha blending
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			// required to use ComputeScreenPos()
			#include "UnityCG.cginc"
			#include "noiseSimplex.cginc"
		
			#pragma vertex vert
			#pragma fragment frag
		 
			// Unity built-in - NOT required in Properties
			sampler2D _CameraDepthTexture;

			float4 _Color;

			float _DepthFactor;

			float _NoiseSize;

			struct vertexInput
			{
				float4 vertex : POSITION;
			};
			
			struct vertexOutput
			{
				float4 pos : SV_POSITION;
				float4 screenPos : TEXCOORD1;
			};
			
			vertexOutput vert(vertexInput input)
			{
			  vertexOutput output;
			
			  // convert obj-space position to camera clip space
			  output.pos = UnityObjectToClipPos(input.vertex + float3(0., snoise(input.vertex+float3(_Time.x*2.,0.,_Time.x*2.)), 0.) * _NoiseSize);
			  // compute depth (screenPos is a float4)
			  output.screenPos = ComputeScreenPos(output.pos);

			  return output;
			}


			bool depthIsNotSky(float depth)
            {
                #if defined(UNITY_REVERSED_Z)
                return (depth > 0.0);
                #else
                return (depth < 1.0);
                #endif
            }

		
			float4 frag(vertexOutput input) : COLOR
			{
			  // sample camera depth texture
			  float4 depthSample = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, input.screenPos);
			  float depth = LinearEyeDepth(depthSample).r;

			  
			  // caculate depth value
			  float foamLine = saturate(_DepthFactor * (depth - input.screenPos.w));
			  
			  // recolor fog with predefined colors
			  float4 col = _Color;
			  col.a = 0.;
				  
				  
				if (depthIsNotSky(depthSample)) {
					col.a = foamLine;
				}
				  

			  
			  
			  return col;
			}
		
		  ENDCG
		}
	}
}