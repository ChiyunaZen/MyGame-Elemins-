using Sydewa;
using System.Collections;
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

    [SerializeField] SymbolAllController symbolManager;

    [SerializeField] UI_Loading ui_Loading;

    private GameData currentGameData; // ゲームデータを保持



    private void Awake()
    {
        // シングルトンの設定
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // symbolManager を探して初期化
            if (symbolManager == null)
            {
                symbolManager = FindObjectOfType<SymbolAllController>();
                if (symbolManager == null)
                {
                    Debug.LogError("SymbolAllController がシーン内に見つかりません。");
                }
            }
        }
        else
        {
            Destroy(gameObject);
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

            if (IsOpenExitDialog)
            {
                CancelExitGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
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
        if (backTitleDialog != null)
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
        //SceneManager.LoadScene("TitleScene");
        Debug.Log("タイトルシーンに遷移します");
        ui_Loading.LoadingScene("TitleScene");
        pauseMenu.ExitPoseMenu();
        // lightingManager.SunDirectionalLight = GameObject.FindWithTag("DirectionalLight").GetComponent<footPrintLight>();
        CancelBackTitle();

    }

    //レベル１ゲーム画面に移る
    public void StartNewGame()
    {
        Debug.Log("レベル１シーンに遷移します");
        ui_Loading.LoadingScene("Level1Scene");
        //  lightingManager.SunDirectionalLight = GameObject.FindWithTag("DirectionalLight").GetComponent<footPrintLight>();
    }

    public void SaveGame()
    {
        if (symbolManager == null)
        {
            Debug.LogError("SaveGame: symbolManager が初期化されていません。");
            return;
        }

        GameData gameData = new GameData
        {
            sceneName = SceneManager.GetActiveScene().name,
            playerPos = GameObject.FindGameObjectWithTag("Player").transform.position,
            eleminData = new EleminData(),
            symbols = symbolManager.GetSymbolDataList(),
            footPrints = FindObjectOfType<FootPrintsAllController>().GetAllFootPrintData(),
            gameTime = SunTimeManager.Instance.lightingManager.TimeOfDay
        };

        EleminController elemin = FindObjectOfType<EleminController>();
        if (elemin != null)
        {
            elemin.EleminDataSet(gameData); // Elemin のデータをセット
        }

        SaveSystem.SaveGame(gameData);

        Debug.Log("ゲームをセーブしました");

      //  SaveDataLog();

    }

    private GameData gameData;  //gameDataを保持
    public void LoadGame()
    {
        GameData loadedData = SaveSystem.LoadGame();

        if (loadedData != null)
        {
            // ゲームデータが存在する場合は、それを保持
            currentGameData = loadedData;
            // シーン遷移後にデータを再設定する
            LoadSceneWithGameData(loadedData);
        }
        else
        {
            // セーブデータが見つからない場合、初期化せずにそのままゲーム開始
            Debug.Log("セーブデータが見つかりません。初期状態からゲームを開始します。");
            StartNewGame();
        }
    }
    private void LoadSceneWithGameData(GameData gameData)
    {
        // ロードするシーンを指定して遷移
        ui_Loading.LoadingScene(gameData.sceneName);
        Debug.Log($"{gameData.sceneName}に遷移します");
    }

    //ロードしたデータに基づいてゲームを設定する
    private void OnEnable()
    {
        ui_Loading.OnSceneLoaded.AddListener(OnSceneLoaded);
    }

    private void OnDisable()
    {
        ui_Loading.OnSceneLoaded.RemoveListener(OnSceneLoaded);
    }

    private void OnSceneLoaded(string sceneName)
    {
        if (currentGameData != null)
        {
           // RestoreGameState(currentGameData);
            StartCoroutine(UpdateGameState(currentGameData));
        }
    }

    private void InstantiateLoadObjects(GameData gameData)
    {

    }

    private void RestoreGameState(GameData gameData)
    {
        
        // プレイヤーの復元
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.LoadPlayerDate(gameData);
        }

        // Eleminデータの復元
        EleminController elemin = FindObjectOfType<EleminController>();
        if (elemin != null)
        {
            elemin.LoadEleminData(gameData.eleminData);  // Eleminのデータを復元
        }

        // 足跡データの復元
        FootPrintsAllController footPrintController = FindObjectOfType<FootPrintsAllController>();
        if (footPrintController != null)
        {
            footPrintController.LoadFootprints(gameData.footPrints);  // 足跡の復元
        }

        // シンボルデータの復元
        SymbolAllController symbolManager = FindObjectOfType<SymbolAllController>();
        if (symbolManager != null)
        {
            symbolManager.LoadSymbolDataList(gameData.symbols);  // シンボルの復元
        }

        // lightingManager.SunDirectionalLight = GameObject.FindWithTag("DirectionalLight").GetComponent<footPrintLight>();
        // ゲーム時間の復元
        SunTimeManager.Instance.LoadSunTimeDate(gameData);
    } 
    
    IEnumerator UpdateGameState(GameData gameData)
    {
        yield return new WaitForSeconds(1f);
        
        // プレイヤーの復元
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.LoadPlayerDate(gameData);
        }

        // Eleminデータの復元
        EleminController elemin = FindObjectOfType<EleminController>();
        if (elemin != null)
        {
            elemin.LoadEleminData(gameData.eleminData);  // Eleminのデータを復元
        }

        // 足跡データの復元
        FootPrintsAllController footPrintController = FindObjectOfType<FootPrintsAllController>();
        if (footPrintController != null)
        {
            footPrintController.LoadFootprints(gameData.footPrints);  // 足跡の復元
        }

        // シンボルデータの復元
        SymbolAllController symbolManager = FindObjectOfType<SymbolAllController>();
        if (symbolManager != null)
        {
            symbolManager.LoadSymbolDataList(gameData.symbols);  // シンボルの復元
        }

        // lightingManager.SunDirectionalLight = GameObject.FindWithTag("DirectionalLight").GetComponent<footPrintLight>();
        // ゲーム時間の復元
        SunTimeManager.Instance.LoadSunTimeDate(gameData);
    }





    void SaveDataLog()
    {
        // セーブデータ内容をデバッグ表示
        GameData savedData = SaveSystem.LoadGame(); // セーブしたデータをロードして確認
        if (savedData != null)
        {
            Debug.Log("現在のシーン: " + savedData.sceneName);
            Debug.Log("プレイヤー位置: " + savedData.playerPos);

            // EleminData の詳細を表示
            if (savedData.eleminData != null)
            {
                Debug.Log("Eleminのデータ:");
                Debug.Log("  位置: " + savedData.eleminData.eleminPos);
                Debug.Log("  アルファ値: " + savedData.eleminData.eleminAlpha);
                Debug.Log("  ライト範囲: " + savedData.eleminData.eleminRange);
                Debug.Log("  ライト強度: " + savedData.eleminData.eleminIntensity);
            }

            // FootPrintData の詳細を表示
            if (savedData.footPrints != null && savedData.footPrints.Count > 0)
            {
                Debug.Log("足跡データ:");
                for (int i = 0; i < savedData.footPrints.Count; i++)
                {
                    FootPrintData footPrint = savedData.footPrints[i];
           //         Debug.Log($"  足跡[{i}] - 位置: {footPrint.position}, 開花状態: {footPrint.isBlooming}");
                    if (footPrint.flowerPositions != null)
                    {
                        for (int j = 0; j < footPrint.flowerPositions.Count; j++)
                        {
            //                Debug.Log($"    花[{j}] - 位置: {footPrint.flowerPositions[j]}, 回転: {footPrint.flowerRotations[j]}");
                        }
                    }
                }
            }
            else
            {
                Debug.Log("足跡データがありません。");
            }

            // SymbolData の詳細を表示
            if (savedData.symbols != null && savedData.symbols.Count > 0)
            {
                Debug.Log("シンボルデータ:");
                for (int i = 0; i < savedData.symbols.Count; i++)
                {
                    SymbolData symbol = savedData.symbols[i];
                    Debug.Log($"  シンボル[{i}] - ID: {symbol.symbolId}, ライト範囲: {symbol.symbolLightRange}, ライト強度: {symbol.symbolLightIntensity}, 点灯状態: {symbol.isLighting}");
                }
            }
            else
            {
                Debug.Log("シンボルデータがありません。");
            }

            Debug.Log("現在時刻: " + savedData.gameTime);
        }
        else
        {
            Debug.Log("セーブデータが見つかりませんでした。");
        }
    }



}



