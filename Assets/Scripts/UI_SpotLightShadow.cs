using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Experimental.GlobalIllumination;

public class UI_SpotLightShadow : MonoBehaviour
{
    public TextMeshProUGUI titleText; //メインテキスト
    public TextMeshProUGUI titleTextShadow; // 追従させたいUIテキスト
    public TextMeshProUGUI menuText;
    public TextMeshProUGUI menuTextShadow;
    

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

            // メインテキストの位置を取得
            Vector3 titleTextPosition = titleText.rectTransform.position;
            Vector3 menuTextPosition = menuText.rectTransform.position;

            // 影の位置をマウスのオフセット量に基づいて計算
            titleTextShadow.rectTransform.position = titleTextPosition + mouseOffset.normalized * shadowOffsetFactor * mouseOffset.magnitude;
            menuTextShadow.rectTransform.position =menuTextPosition +mouseOffset.normalized * shadowOffsetFactor * mouseOffset.magnitude;
        }
    }
}
