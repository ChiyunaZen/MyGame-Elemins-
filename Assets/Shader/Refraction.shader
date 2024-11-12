Shader "Custom/Refraction"
{
    Properties
    {
        _RefractionAmount ("Refraction Amount", Range(0, 0.1)) = 0.05
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 200

        GrabPass { "_GrabTexture" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
                float4 grabUV : TEXCOORD1;
            };

            sampler2D _GrabTexture;
            float _RefractionAmount;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.grabUV = ComputeGrabScreenPos(o.pos);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Grabテクスチャから背景ピクセルを取得し、UVをずらして屈折効果を適用
                float2 offset = (i.uv - 0.5) * _RefractionAmount;
                fixed4 col = tex2D(_GrabTexture, i.grabUV.xy + offset);
                return col;
            }
            ENDCG
        }
    }
    Fallback "Transparent"
}
