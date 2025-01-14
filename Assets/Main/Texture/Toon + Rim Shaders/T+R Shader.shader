Shader "Custom/Item Shader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _RampTex ("Ramp Texture", 2D) = "White" {}
        _RimColor ("Rim Color", Color) = (0, 0, 0, 0)
        _RimPower ("Rim Power", Range(0.5, 8.0)) = 3.0
        _SinFrequency ("Sin Frequency", Range(0, 5)) = 0.5
        _SinAmplitude ("Sin Amplitude", Range(0, 5)) = 0.5
        _Active ("Active", Range(0, 1)) = 1
    }
    SubShader
    {
        CGPROGRAM
        #pragma surface surf ToonRamp

        float4 _Color;
        sampler2D _RampTex;
        float4 _RimColor;
        float _RimPower;
        float _SinFrequency;
        float _SinAmplitude;
        float _Active;

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
            float2 uv_MainTex;
            float3 viewDir;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
            float SinValue = sin((_SinFrequency * _Time.y) + IN.uv_MainTex.x) * _SinAmplitude;

            if (_Active > 0.5)
            {
                o.Emission = (_RimColor.rgb + SinValue) * pow(rim, _RimPower);
            }
            else
            {
                o.Emission = float3(1,1,1);
            }
            o.Albedo = _Color.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}