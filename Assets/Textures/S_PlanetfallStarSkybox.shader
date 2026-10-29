Shader "Planetfall/Procedural Star Skybox"
{
    Properties
    {
        _Density ("Star Density", Range(0.02, 0.3)) = 0.09
        _StarSize ("Star Size", Range(0.02, 0.2)) = 0.08
        _TwinkleSpeed ("Twinkle Speed", Range(0.1, 5)) = 1.5
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Background"
            "RenderType" = "Background"
            "PreviewType" = "Skybox"
        }

        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float _Density;
            float _StarSize;
            float _TwinkleSpeed;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 direction : TEXCOORD0;
            };

            float hash21(float2 p)
            {
                return frac(sin(dot(p, float2(127.1, 311.7)))
                    * 43758.5453);
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.direction = v.vertex.xyz;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 dir = normalize(i.direction);

                float2 uv = float2(
                    atan2(dir.z, dir.x) / 6.2831853 + 0.5,
                    asin(clamp(dir.y, -1, 1)) / 3.14159265 + 0.5
                );

                float2 grid = uv * float2(100, 50);
                float2 cell = floor(grid);
                float2 localPos = frac(grid);

                float randomValue = hash21(cell);
                float2 starPos = float2(
                    hash21(cell + 13.1),
                    hash21(cell + 37.7)
                );

                float distanceToStar = length(localPos - starPos);

                float star = step(1.0 - _Density, randomValue);
                star *= 1.0 - smoothstep(
                    _StarSize * 0.35,
                    _StarSize,
                    distanceToStar
                );

                float phase = hash21(cell + 71.3) * 6.2831853;
                float speed = 0.6 + hash21(cell + 19.7) * 1.8;
                float twinkle = 0.35 + 0.65 *
                    (0.5 + 0.5 * sin(_Time.y * _TwinkleSpeed * speed + phase));

                float3 starColor = float3(1, 1, 1);
                float colorChoice = hash21(cell + 91.2);

                if (colorChoice < 0.20)
                    starColor = float3(0.45, 0.65, 1.0);
                else if (colorChoice < 0.40)
                    starColor = float3(0.85, 0.45, 1.0);
                else if (colorChoice < 0.55)
                    starColor = float3(1.0, 0.45, 0.75);
                else if (colorChoice < 0.65)
                    starColor = float3(1.0, 0.70, 0.40);

                float3 background = float3(0.001, 0.001, 0.003);
                return float4(background + star * starColor * twinkle, 1);
            }
            ENDCG
        }
    }

    Fallback Off
}