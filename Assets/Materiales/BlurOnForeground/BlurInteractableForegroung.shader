Shader "Custom/BlurPostProcess"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" { }
        _BlurStrength ("Blur Strength", Range(0, 10)) = 2.0
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            uniform float _BlurStrength;
            sampler2D _MainTex;
            float4 _MainTex_TexelSize;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                float2 texOffset = _MainTex_TexelSize.xy * _BlurStrength;

                half4 color = tex2D(_MainTex, i.uv);
                color += tex2D(_MainTex, i.uv + float2(texOffset.x, 0));
                color += tex2D(_MainTex, i.uv + float2(-texOffset.x, 0));
                color += tex2D(_MainTex, i.uv + float2(0, texOffset.y));
                color += tex2D(_MainTex, i.uv + float2(0, -texOffset.y));
                color *= 0.2;

                return color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
