using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // シングルトンインスタンス
    public static GameManager Instance { get; private set; }

    //bool isTitleScene; //タイトルシーンかどうか

    [SerializeField] UI_PauseMenu pauseMenu;

    [SerializeField] GameObject backTitleDialog; //タイトルに戻る確認用ダイアログパネル

    [SerializeField] GameObject exitDialog;  // 確認ダイアログ用の UI パネル
    public bool IsOpenExitDialog { get; private set; } //修了確認用ダイアログが開いているか

    [SerializeField] AllSymbolManager symbolManager;


    private void Awake()
    {
        // シングルトンの設定
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンをまたいでも破棄されないようにする

            // 必要なコンポーネントを探して保持


            // exitDialog.SetActive(false);

        }
        else
        {
            Destroy(gameObject); // 二重に存在する場合は破棄
            return;
        }
    }

    void Start()
    {
        backTitleDialog.SetActive(false);
        exitDialog.SetActive(false); //修了確認ダイアログは非アクティブ
        IsOpenExitDialog = false;
        
    }
    // Update is called once per frame
    void Update()

    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //現在がタイトルシーンではないときはPoseメニューを表示
            if (SceneManager.GetActiveScene().name != "TitleScene")
            {
                pauseMenu.ToggleShowPose();
            }

            if(IsOpenExitDialog)
            {
                CancelExitGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGame();
            Debug.Log("ゲームをセーブしました");

            // セーブデータ内容をデバッグ表示
            GameData savedData = SaveSystem.LoadGame(); // セーブしたデータをロードして確認
            if (savedData != null)
            {
                Debug.Log("セーブデータ内容:");
                Debug.Log("シーン名: " + savedData.sceneName);
                Debug.Log("プレイヤー位置: " + savedData.playerPos);
                Debug.Log("ゲーム時間: " + savedData.gameTime);
            }
        }

    }

    //タイトルに戻る確認ダイアログの表示
    public void ShowBackTitleDailog()
    {
        if (backTitleDialog != null && !backTitleDialog.activeSelf)
        {
            backTitleDialog.SetActive(true);
        }
    }

    public void CancelBackTitle()
    {
        if (backTitleDialog != null )
        {
            backTitleDialog.SetActive(false);
        }
    }
    

    //ゲーム終了確認ダイアログの表示
    public void ShowExitDialog()
    {
        if (exitDialog != null && !exitDialog.activeSelf)
        {
            exitDialog.SetActive(true);
            IsOpenExitDialog = true;
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
            exitDialog.SetActive(false);
            IsOpenExitDialog = false; // 終了リクエストフラグを解除
        }
    }

    private void OnApplicationQuit()
    {
        // 確認ダイアログが開かれていない場合はアプリケーション終了をキャンセル
        if (!IsOpenExitDialog)
        {
            CancelExitGame();
        }
    }

    //タイトルシーンに戻る
    public void BackTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
        
        pauseMenu.ExitPoseMenu();
        CancelBackTitle();
        
    }

    //レベル１ゲーム画面に移る
    public void StertNewGame()
    {
        SceneManager.LoadScene("Level1Scene");
    }

    public void SaveGame()
    {
        GameData gameData = new GameData
        {
            sceneName = SceneManager.GetActiveScene().name,
            playerPos = GameObject.FindGameObjectWithTag("Player").transform.position,
            eleminData = new EleminData(),
            symbols = new AllSymbolManager().GetSymbolDataList(),
            footPrints = FindObjectOfType<FootPrintsAllController>().GetAllFootPrintData(),
            gameTime = SunTimeManager.Instance.lightingManager.TimeOfDay
        };

        EleminController elemin = FindObjectOfType<EleminController>();
        if (elemin != null)
        {
            elemin.EleminDataSet(gameData); // Elemin のデータをセット
        }

        SaveSystem.SaveGame(gameData);
        
    }

    private void InitializeGameData()
    {
        // プレイヤーの初期位置を設定
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = new Vector3(0f, 0f, 0f); // 初期位置
        }

        // Eleminの初期設定
        EleminController elemin = FindObjectOfType<EleminController>();
        if (elemin != null)
        {
            elemin.InitializeEleminData(); // 初期データ設定メソッド（データの設定処理を別途作成）
        }

        // 足跡データの初期化
        FootPrintsAllController footPrintController = FindObjectOfType<FootPrintsAllController>();
        if (footPrintController != null)
        {
            footPrintController.InitializeFootPrints(); // 足跡の初期化
        }

        // シンボルの初期設定
        AllSymbolManager symbolManager = FindObjectOfType<AllSymbolManager>();
        if (symbolManager != null)
        {
            symbolManager.InitializeSymbolData(); // シンボルの初期化
        }

        // ゲーム時間の初期設定
        SunTimeManager.Instance.lightingManager.TimeOfDay = 2f;
    }

    public void DeleteGameDataAndExit()
    {
        SaveSystem.DeleteSaveData();
        ExitGame();
    }

   

   
}



