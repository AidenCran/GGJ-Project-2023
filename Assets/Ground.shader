Shader "Unlit/Ground"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MapScale("Scale", float) = 1.
        _ScrollSpeed("Texture Scroll Speed",Vector) = (0.,0.,0.,0.)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
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
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD2;
                float3 worldPos : TEXCOORD3;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _MapScale;
            float3 _ScrollSpeed;

            

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.normal = mul(unity_ObjectToWorld,v.normal);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 bf = normalize(abs(i.normal));
                bf /= dot(bf, (float3)1);
                float3 transformedWorldPos = i.worldPos + _ScrollSpeed * _Time.y;
                float2 tx = transformedWorldPos.yz * _MapScale;
                float2 ty = transformedWorldPos.zx * _MapScale;
                float2 tz = transformedWorldPos.xy * _MapScale;

                // Base color
                half4 cx = tex2D(_MainTex, tx) * bf.x;
                half4 cy = tex2D(_MainTex, ty) * bf.y;
                half4 cz = tex2D(_MainTex, tz) * bf.z;
                half4 color = (cx + cy + cz);
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return color;
            }
            ENDCG
        }
    }
}
