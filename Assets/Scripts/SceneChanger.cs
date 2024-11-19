using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    //ゲーム画面に移る
   public void StertNewGame()
    {
        SceneManager.LoadScene("Level1Scene");
    }

    //ゲームを終了するメソッド
    public void ExitGame() 
    {
        Application.Quit();

        // エディタで実行中の場合
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // エディタのプレイモードを停止
#endif
    }
}
