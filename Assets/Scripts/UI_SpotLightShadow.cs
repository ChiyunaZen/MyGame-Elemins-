using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_SpotLightShadow : MonoBehaviour
{
    public TextMeshProUGUI titleTextshadow; // 追従させたいUIテキスト
    public Vector2 offset; // マウス位置からのオフセット

    void Update()
    {
        // マウスのスクリーン座標を取得
        Vector2 mousePosition = Input.mousePosition;

        // オフセットを加えてUIテキストの位置を調整
       titleTextshadow.rectTransform.position = mousePosition + offset;
    }
}
