using Sydewa;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // シングルトンインスタンス
    public static GameManager Instance { get; private set; }

    //bool isTitleScene; //タイトルシーンかどうか

    [SerializeField] UI_PauseMenu poseMenu; 

    [SerializeField] GameObject exitDialog;  // 確認ダイアログ用の UI パネル
   public bool isOpenExitDialog = false; //修了確認用ダイアログが開いているか


    private void Awake()
    {
        // シングルトンの設定
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンをまたいでも破棄されないようにする
                                          
            // 必要なコンポーネントを探して保持
           
           
            exitDialog.SetActive(false);
        }
        else
        {
            Destroy(gameObject); // 二重に存在する場合は破棄
            return;
        }
    }

    void Start()
    {
        exitDialog.SetActive(false); //修了確認ダイアログは非アクティブ
      //  eleminController = GameObject.FindWithTag("SubCharacter").GetComponent<EleminController>();
       
       // Enemy = GameObject.FindWithTag("Enemy");

    }
    // Update is called once per frame
    void Update()

    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //現在がタイトルシーンではないときはPoseメニューを表示
            if(SceneManager.GetActiveScene().name != "TitleScene")
            {
                poseMenu.ToggleShowPose();
            }
        }

    }

   

    //ゲーム終了確認ダイアログの表示
    public void ShowExitDialog()
    {
        if (exitDialog != null && !exitDialog.activeSelf)
        {
            exitDialog.SetActive(true);
            isOpenExitDialog = true;
        }
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

    //終了をキャンセルする場合
    public void CancelExitGame()
    {
        if (exitDialog != null)
        {
            exitDialog.SetActive(false );
            isOpenExitDialog = false; // 終了リクエストフラグを解除
        }
    }

    private void OnApplicationQuit()
    {
        // 確認ダイアログが開かれていない場合はアプリケーション終了をキャンセル
        if (!isOpenExitDialog)
        {
            CancelExitGame();
        }
    }

    //タイトルシーンに戻る
    public void BackTitleScene()
    {
        poseMenu.ExitPoseMenu();
        SceneManager.LoadScene("TitleScene");
    }

    //レベル１ゲーム画面に移る
    public void StertNewGame()
    {
        SceneManager.LoadScene("Level1Scene");
    }


}


