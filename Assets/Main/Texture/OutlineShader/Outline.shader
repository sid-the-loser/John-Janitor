// Written by Sidharth S, referenced from Canvas

// This shader is for toon shading and color grading. It makes its own lighting system to make the sharp shade changes-
// the toon shading possible!

Shader "Custom/Outline Effect"
{
    Properties
    {
        _MainTex ("Albedo Texture", 2D) = "white" {} 
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 0)
        _Outline ("Outline Width", Range(0, 0.1)) = 0.005
    }
    SubShader
    {
        ZWrite Off
        
        CGPROGRAM

        #pragma surface surf Lambert vertex:vert

        sampler2D _MainTex; // getting the ramp texture from properties
        
        struct Input {
            float2 uv_MainTex;
        };
        float _Outline;
        float4 _OutlineColor;
        void vert (inout appdata_full v) {
            v.vertex.xyz += v.normal * _Outline;
        }
        
        void surf (Input IN, inout SurfaceOutput o) {
            o.Emission = _OutlineColor.rgb;
        }
        ENDCG
        
        ZWrite On
        CGPROGRAM
        
        #pragma surface surf Lambert

        struct Input
        {
            float2 uv_MainTex;
        };

        sampler2D _MainTex;

        void surf(Input IN, inout SurfaceOutput o)
        {
            o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
        }

        ENDCG
    }
    FallBack "Diffuse" // fallback, in case something breaks in the shader that we wrote
}