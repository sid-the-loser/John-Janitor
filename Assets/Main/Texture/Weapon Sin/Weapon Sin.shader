Shader "Custom/Weapon Sin"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _RimColor ("Rim Color", Color) = (1,1,1,1)
        _RimPower ("Rim Power", Range(1, 10)) = 3
        _PulseSpeed ("Pulse Speed", Float) = 2.0
        _Active ("Active", Range(0, 1)) = 1
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MainTex;
        fixed4 _RimColor;
        float _RimPower;
        float _PulseSpeed;
        float _Active;

        struct Input
        {
            float2 uv_MainTex;
            float3 viewDir;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            
            fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = tex.rgb;
            if (_Active > 0.5)
            {
            // Calculate rim based on view direction and normal
            float rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
            
            // Pulse factor using sine wave
            float pulse = 0.5 + 0.5 * sin(_Time.y * _PulseSpeed);

            // Apply rim lighting
            o.Emission = _RimColor.rgb * pow(rim, _RimPower) * pulse;
            }
        }
        ENDCG
    }
    FallBack "Diffuse"
}
