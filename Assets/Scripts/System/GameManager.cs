using Sydewa;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // シングルトンインスタンス
    public static GameManager Instance { get; private set; }

    EleminController eleminController;
    [SerializeField] FootPrintsAllController footPrintsAllController;
    GameObject directionalLight;
    [SerializeField] LightingManager lightingManager;

    [SerializeField] float startTimeOfDay = 2;
    [SerializeField] float targetTimeOfDay = 12f;
    [SerializeField] float sunRiseSpeed = 1f;
    [SerializeField] float startBloomSunTime = 6f;
    GameObject Enemy;

    [SerializeField] GameObject exitDialog;  // 確認ダイアログ用の UI パネル
    bool isOpenExitDialog = false; //修了確認用ダイアログが開いているか


    private void Awake()
    {
        // シングルトンの設定
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンをまたいでも破棄されないようにする
        }
        else
        {
            Destroy(gameObject); // 二重に存在する場合は破棄
        }
    }

    void Start()
    {
        exitDialog.SetActive(false); //修了確認ダイアログは非アクティブ
        eleminController = GameObject.FindWithTag("SubCharacter").GetComponent<EleminController>();
        lightingManager.TimeOfDay = startTimeOfDay;
        Enemy = GameObject.FindWithTag("Enemy");

    }
    // Update is called once per frame
    void Update()

    {


    }

    public void Ending()
    {
      

        StartCoroutine(SunRise());

    }

    IEnumerator SunRise()
    {

        while (lightingManager.TimeOfDay < targetTimeOfDay)
        {
            // 時刻を徐々に増加
            lightingManager.TimeOfDay += sunRiseSpeed * Time.deltaTime;

            //設定時刻になったら花を咲かせるメソッドを呼び出す
            if (lightingManager.TimeOfDay >= startBloomSunTime)
            {
                footPrintsAllController.GetFootPrintsFlowers();
            }

            // 次のフレームまで待機
            yield return null;
        }

        // 最終的に目標時刻にそろえる
        lightingManager.TimeOfDay = targetTimeOfDay;


    }

    //ゲーム終了時の処理
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
            exitDialog.SetActive(false);
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
}


