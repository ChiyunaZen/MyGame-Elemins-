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

   public  void　GameStartButton()
    {
        // GameManagerのインスタンスが存在すれば、LoadGameメソッドを実行
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadGame();
        }
        else
        {
            Debug.LogError("GameManagerのインスタンスが見つかりません！");
        }
    }
}

