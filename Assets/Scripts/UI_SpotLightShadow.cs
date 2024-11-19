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
    public TextMeshProUGUI menuText1;
    public TextMeshProUGUI menuTextShadow1;
    public TextMeshProUGUI menuText2;
    public TextMeshProUGUI menuTextShadow2;
    public TextMeshProUGUI menuText3;
    public TextMeshProUGUI menuTextShadow3;
    

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
            Vector3 menuTextPosition1 = menuText1.rectTransform.position;
            Vector3 menuTextPosition2 = menuText2.rectTransform.position;
            Vector3 menuTextPosition3 = menuText3.rectTransform.position;

            // 影の位置をマウスのオフセット量に基づいて計算
            titleTextShadow.rectTransform.position = titleTextPosition + mouseOffset.normalized * shadowOffsetFactor * mouseOffset.magnitude;
            menuTextShadow1.rectTransform.position =menuTextPosition1 +mouseOffset.normalized * shadowOffsetFactor * mouseOffset.magnitude;
            menuTextShadow2.rectTransform.position =menuTextPosition2 +mouseOffset.normalized * shadowOffsetFactor * mouseOffset.magnitude;
            menuTextShadow3.rectTransform.position =menuTextPosition3 +mouseOffset.normalized * shadowOffsetFactor * mouseOffset.magnitude;
        }
    }
}
