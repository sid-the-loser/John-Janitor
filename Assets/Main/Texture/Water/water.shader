Shader "Custom/water"
{
    Properties
    {
        _MainTex("Diffuse", 2D) = "white" {}
        _FoamTex("Foam Diffuse", 2D) = "white" {}
        _BumpMap("Normal Map", 2D) = "bump" {}
        _mySlider ("Bump factor", Range(0,10)) = 1 
        _Tint("Colour Tint", Color) = (1, 1, 1, 1 )
        _Freq("Frequency", Range (0,5)) = 3
        _Speed("Speed", Range(0, 100)) = 10
        _Amp("Amplitude", Range(0,1)) = 0.5
    }
    SubShader
    {
        CGPROGRAM
        #pragma surface surf Lambert vertex:vert
        #pragma target 3.0

         struct Input
         {
             float2 uv_MainTex;
            float2 uv_FoamTex;
            float2 uv_BumpMap;
             float3 vertColor;
         };

         float3 _Tint;
         float _Freq;
         float _Speed;
         float _Amp;
        half _mySlider;

         struct appdata
         {
             float4 vertex: POSITION;
             float3 normal: NORMAL;
             float4 tangent : TANGENT;
             float4 texcoord: TEXCOORD0;
             float4 texcoord1: TEXCOORD1;
             float4 texcoord2: TEXCOORD2;
         };

         void vert(inout appdata v, out Input o)
         {
             UNITY_INITIALIZE_OUTPUT(Input, o);
             float t = _Time.y * _Speed;
             float waveHeight = sin(t + v.vertex.x * _Freq) * _Amp + sin(t * 2 + v.vertex.x * _Freq * 2) * _Amp;

             v.vertex.y = v.vertex.y + waveHeight;
             v.normal = normalize(float3(v.normal.x + waveHeight, v.normal.y, v.normal.z));
             o.vertColor = waveHeight + 2;
         }

         sampler2D _MainTex;
        sampler2D _BumpMap;
        sampler2D _FoamTex;

        float _ScrollX;
        float _ScrollY;

         void surf(Input IN, inout SurfaceOutput o)
         {
             _ScrollX -= _Time * 5;
             _ScrollY -= _Time * 5;
             
             float4 c = tex2D(_MainTex, IN.uv_MainTex + float2(_ScrollX, _ScrollY));
             float4 f = tex2D(_FoamTex, IN.uv_FoamTex + float2(-_ScrollX, -_ScrollY));
             float4 normalTex = tex2D(_BumpMap, IN.uv_BumpMap + float2(_ScrollX, _ScrollY));
             
             o.Albedo = (c * IN.vertColor).rgb;
             o.Emission = f.rgb;
             o.Normal = UnpackNormal(normalTex);
             o.Normal *= float3(_mySlider,_mySlider,1);
         }
         ENDCG
    }
    FallBack "Diffuse"
}