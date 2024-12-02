using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Experimental.GlobalIllumination;

public class UI_SpotLightShadow : MonoBehaviour
{
    public TextMeshProUGUI[] texts; // テキスト
    public TextMeshProUGUI[] textShadows; // tテキストの影


    public float shadowOffsetFactor = 0.1f; // 影のオフセット量

    [SerializeField] SelectMenuPointer selectMenuPointer;

   

    void Update()
    {
        if (selectMenuPointer.IsClicked)
        {
            // マウスのスクリーン座標を取得
            Vector3 mousePosition = Input.mousePosition;

            // 画面中心からの相対位置を計算
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Vector3 mouseOffset = mousePosition - screenCenter;

            // 各テキストとその影をループで処理
            AdjustTextShadows(texts, textShadows, mouseOffset);

        }
    }

    // テキスト影の位置を一括で調整するメソッド
    private void AdjustTextShadows(TextMeshProUGUI[] texts, TextMeshProUGUI[] shadows, Vector3 mouseOffset)
    {
        for (int i = 0; i < texts.Length; i++)
        {
            // 各テキストの位置
            Vector3 textPosition = texts[i].rectTransform.position;

            // 影の位置を計算して設定
            shadows[i].rectTransform.position = textPosition + mouseOffset.normalized * shadowOffsetFactor * mouseOffset.magnitude;
        }
    }
}
