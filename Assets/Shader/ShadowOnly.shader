Shader "Custom/ShadowOnly"
{
    Properties
    {
        // 特にプロパティは必要ありません
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        Pass
        {
            // レンダリングそのものをスキップ
            ColorMask 0
        }
    }
    Fallback "Transparent/Cutout/Diffuse"
}
