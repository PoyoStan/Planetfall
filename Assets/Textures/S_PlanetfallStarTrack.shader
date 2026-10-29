Shader "Planetfall/Star Track"
{
    Properties
    {
        _BaseColor ("Track Color", Color) = (0.12, 0.04, 0.25, 1)
        _StarDensity ("Star Density", Range(0.1, 3)) = 0.8
        _StarSize ("Star Size", Range(0.01, 0.2)) = 0.045
        _TwinkleSpeed ("Twinkle Speed", Range(0.1, 5)) = 1.5
        _StarBrightness ("Star Brightness", Range(0, 5)) = 2
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }

        Cull Back
        ZWrite On

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 _BaseColor;
            float _StarDensity;
            float _StarSize;
            float _TwinkleSpeed;
            float _StarBrightness;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD0;
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
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 grid = i.worldPos.xz * _StarDensity;
                float2 cell = floor(grid);
                float2 localPos = frac(grid);

                float randomValue = hash21(cell);
                float2 starPos = float2(
                    hash21(cell + 13.1),
                    hash21(cell + 37.7)
                );

                float distanceToStar = length(localPos - starPos);

                float star = step(0.88, randomValue);
                star *= 1.0 - smoothstep(
                    _StarSize * 0.35,
                    _StarSize,
                    distanceToStar
                );

                float phase = hash21(cell + 71.3) * 6.2831853;
                float speed = 0.6 + hash21(cell + 19.7) * 1.8;

                float twinkle = 0.25 + 0.75 *
                    (0.5 + 0.5 * sin(
                        _Time.y * _TwinkleSpeed * speed + phase
                    ));

                float3 color = _BaseColor.rgb
                    + star * twinkle * _StarBrightness;

                return float4(saturate(color), _BaseColor.a);
            }
            ENDCG
        }
    }

    FallBack Off
}