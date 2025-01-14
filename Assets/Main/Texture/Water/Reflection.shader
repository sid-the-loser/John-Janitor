Shader "Custom/Reflection"
{
    Properties
    {
        _BaseTex ("Base Texture", 2D) = "white" {}
        _ReflectionTex ("Reflection Texture", 2D) = "white" {}
        _PuddleIntensity ("Puddle Intensity", Range(0, 1)) = 0.5
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _BaseTex;
        sampler2D _ReflectionTex;
        float _PuddleIntensity;

        struct Input
        {
            float2 uv_BaseTex;
            float3 worldPos;
            float3 worldRefl;
        };

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // Sample the base texture
            fixed4 baseColor = tex2D(_BaseTex, IN.uv_BaseTex);

            // Calculate reflection vector
            float3 viewDir = normalize(UnityWorldSpaceViewDir(IN.worldPos));
            float3 reflectDir = reflect(viewDir, normalize(o.Normal));

            // Sample the reflection texture
            float2 reflectionCoords = reflectDir.xy * 0.5 + 0.5;
            fixed4 reflectionColor = tex2D(_ReflectionTex, reflectionCoords);

            // Mix the base color and reflection color based on puddle intensity
            o.Albedo = lerp(baseColor.rgb, reflectionColor.rgb, _PuddleIntensity);
            o.Metallic = 0.0;
            o.Smoothness = 0.8;
            o.Alpha = baseColor.a;
        }
        ENDCG
    }
    FallBack "Diffuse"

}