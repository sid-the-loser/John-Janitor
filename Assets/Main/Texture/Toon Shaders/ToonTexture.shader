Shader "Custom/ToonTexture"
{
    Properties
    {
        _myColor ("Sample Color", Color) = (1, 1, 1, 1)
        _myRange ("Sample Range", Range(0, 5)) = 1
        _myTex ("Sample Texture", 2D) = "white" {}
        _myCube ("Sameple Cube", CUBE) = ""{}
        _myFloat ("Sample Float", Float) = 0.5
        _myVector ("Sample Vector", Vector) = (0.5, 1, 1, 1)
    }
    SubShader
    {
        CGPROGRAM
        #pragma surface surf Lambert

        fixed4 _myColor;
        half _myRange;
        sampler2D _myTex;
        samplerCUBE _myCube;
        float _myFloat;
        float4 _myVector;

        struct Input
        {
            float2 uv_myTex;
            float3 worldRefl;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            o.Albedo = (tex2D(_myTex, IN.uv_myTex) * _myRange).rgb;
            o.Emission = texCUBE(_myCube, IN.worldRefl).rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}