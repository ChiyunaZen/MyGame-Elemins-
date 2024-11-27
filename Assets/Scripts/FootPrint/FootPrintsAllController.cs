using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FootPrintsAllController : MonoBehaviour
{
    FootPrintController[] footPrints;
    [SerializeField] float bloomInterval = 0.1f; //次の花が咲くまでの待ち時間

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void GetFootPrintsFlowers()
    {
        footPrints = FindObjectsByType<FootPrintController>(FindObjectsSortMode.None);

        // ヒエラルキー順に並べ替える
        footPrints = footPrints.OrderBy(fp => fp.transform.GetSiblingIndex()).ToArray();

        StartCoroutine(BloomFlowersInSequence());

    }

    IEnumerator BloomFlowersInSequence()
    {
        // 逆順でfootPrintsを処理
        for (int i = footPrints.Length - 1; i >= 0; i--)
        {
            // FootPrintControllerを取得して花を咲かせる
            FootPrintController footPrintController = footPrints[i];
            footPrintController.Bloomflowers();

            // 次のプレハブの花を生成するまで遅延を入れる
            yield return new WaitForSeconds(bloomInterval);  //インターバルをとる
        }
    }

    //レベル1シーンを読み込むメソッド
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
