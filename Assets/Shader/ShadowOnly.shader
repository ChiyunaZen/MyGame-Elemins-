Shader "Custom/ShadowOnly"
{
    Properties
    {
        // ���Ƀv���p�e�B�͕K�v����܂���
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        Pass
        {
            // �����_�����O���̂��̂��X�L�b�v
            ColorMask 0
        }
    }
    Fallback "Transparent/Cutout/Diffuse"
}
