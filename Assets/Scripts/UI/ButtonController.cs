using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonController : MonoBehaviour
{
   
    public UnityEvent buttonSelectEvent; // アニメーション終了時に呼ばれるイベント

    // アニメーションイベント用メソッド
    public void OnAnimationEnd()
    {
        if (buttonSelectEvent != null)
        {
            buttonSelectEvent.Invoke(); // ボタン固有の処理を実行
        }
    }
}

