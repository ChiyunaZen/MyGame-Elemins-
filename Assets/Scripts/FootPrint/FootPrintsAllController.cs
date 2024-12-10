using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class FootPrintsAllController : MonoBehaviour, ISceneLoadCheck
{
    FootPrintController[] footPrints;
    [SerializeField] float bloomInterval = 0.1f; //次の花が咲くまでの待ち時間

    public bool isFootPrintAllControllerActive;

    public List<FootPrintController> LoadFootPrints = new List<FootPrintController>();  // ロードした足跡のリスト
    public FootPrintController footPrintPrefab;

    

    // Start is called before the first frame update
    void Start()
    {
        footPrints = FindObjectsByType<FootPrintController>(FindObjectsSortMode.None);
        isFootPrintAllControllerActive = true;
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

    //保存用のデータをリスト化して渡す
    public List<FootPrintData> GetAllFootPrintData()
    {
        FootPrintController[] footPrints = FindObjectsByType<FootPrintController>(FindObjectsSortMode.None);

        List<FootPrintData> footPrintDataList = new List<FootPrintData>();
        foreach (var footPrint in footPrints)
        {
            footPrintDataList.Add(footPrint.GetFootPrintData());
        }

        return footPrintDataList;
    }



    // セーブデータから足跡を復元するメソッド
    public void LoadFootprints(List<FootPrintData> footprintDataList)
    {
     

        // 足跡データを基に実際の足跡を復元
        foreach (var footprintData in footprintDataList)
        {
           
                FootPrintController footprint = Instantiate(footPrintPrefab, footprintData.position, Quaternion.identity);
                footprint.LoadFootPrintData(footprintData);
                footprint.LightDestroy();
                LoadFootPrints.Add(footprint);

                Debug.Log($"足跡を追加: {footprint}");
        }

       // isLoadAllFootPrints = true; //すべての足跡を復元完了
        Debug.Log($"終了：復元済みの足跡数 {LoadFootPrints.Count}");

    }

    public bool IsReady()
    {
       // Debug.Log($"isFootPrintAllControllerActive: {isFootPrintAllControllerActive}, isLoadAllFootPrints: {isLoadAllFootPrints}, LoadFootPrints.Count: {LoadFootPrints.Count}");
        return isFootPrintAllControllerActive; 
    }

    
}
