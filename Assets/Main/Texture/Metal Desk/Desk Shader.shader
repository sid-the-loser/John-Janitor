    Shader "Custom/Desk Shader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _SpecColor ("Specular", Color) = (1,1,1,1)
    }
    SubShader
    {
        CGPROGRAM
        #pragma surface surf StandardSpecular

        sampler2D _MainTex;
        fixed4 _Color;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandardSpecular o)
        {
            o.Albedo = _Color.rgb;
            o.Smoothness = tex2D(_MainTex, IN.uv_MainTex);
            o.Specular = _SpecColor.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
