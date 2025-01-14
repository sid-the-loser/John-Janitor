Shader "Custom/Rim lighting Shader"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _RimColor("Rim Color", Color) = (0, 0.5, 0.5, 0.0)
        _RimPower("Rim Power", Range(0.5, 8.0)) = 3.0
        _RampTex("Ramp Texture", 2D) = "White" {}
        _ActiveRim("Active Rim", Range(0, 1)) = 1
        
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _Outline ("Outline Width", Range(.002,0.1)) = 0.005
    }
    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
        }
        Pass
        {
            ZWrite On
            ColorMask 0
        }
        CGPROGRAM
        #pragma surface surf ToonRamp

        float4 _Color;
        float4 _RimColor;
        float _RimPower;
        sampler2D _RampTex;
        float _ActiveRim;

        float4 LightingToonRamp(SurfaceOutput s, fixed3 lightDir, fixed atten)
        {
            float diff = dot(s.Normal, lightDir);
            float h = diff * 0.5 + 0.5;
            float2 rh = h;
            float3 ramp = tex2D(_RampTex, rh).rgb;

            float4 c;
            c.rgb = s.Albedo * _LightColor0.rgb * (ramp);
            c.a = s.Alpha;
            return c;
        }
        struct Input
        {
            float3 viewDir;
        };
        void surf(Input IN, inout SurfaceOutput o)
        {
            if (_ActiveRim > 0.5)
            {
                half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
                o.Alpha = pow(rim, _RimPower);
                o.Emission = _RimColor.rgb * pow(rim, _RimPower) * 10;
            }
            o.Albedo = _Color.rgb;
        }
        ENDCG
    }
    Fallback "Diffuse"
}