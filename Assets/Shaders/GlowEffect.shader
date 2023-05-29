Shader "Custom/GlowEffect"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _GlowColor ("Glow Color", Color) = (1, 1, 1, 1)
        _GlowThreshold ("Glow Threshold", Range(0, 1)) = 0.5
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            ZWrite Off
            Blend SrcAlpha One
            ColorMask RGB
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _GlowColor;
            float _GlowThreshold;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, i.uv);

                // Check if the current pixel is below the glow threshold
                if (color.a < _GlowThreshold)
                {
                    // Emit the glow color
                    color.rgb = _GlowColor.rgb;
                }

                return color;
            }
            ENDCG
        }
    }
}
