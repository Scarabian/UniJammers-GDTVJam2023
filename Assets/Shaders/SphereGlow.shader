Shader "Custom/SphereGlow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _EmissionColor ("Emission Color", Color) = (1, 1, 1, 1)
        _EmissionIntensity ("Emission Intensity", Range(0, 10)) = 1
    }
 
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
 
        CGPROGRAM
        #pragma surface surf Lambert
 
        sampler2D _MainTex;
        fixed4 _Color;
        fixed4 _EmissionColor;
        float _EmissionIntensity;
 
        struct Input
        {
            float2 uv_MainTex;
        };
 
        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Emission = _EmissionColor.rgb * _EmissionIntensity;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
