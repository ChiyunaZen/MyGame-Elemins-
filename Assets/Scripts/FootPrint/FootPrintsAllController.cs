using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class FootPrintsAllController : MonoBehaviour
{
    FootPrintController[] footPrints;
    [SerializeField] float bloomInterval = 0.1f; //次の花が咲くまでの待ち時間
    Slider slider;

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

    public List<FootPrintController> LoadFootPrints;  //ロードした足跡のリスト
    public FootPrintController footPrintPrefab;

    // セーブデータから足跡を復元するメソッド
    public void LoadFootprints(List<FootPrintData> footprintDataList)
    {
        UI_Loading ui_Loading = GameObject.FindAnyObjectByType<UI_Loading>();

        ui_Loading.slider.value = 0.2f;
        float totalFootprints = footprintDataList.Count; // データの数に応じた進捗
        float progressPerFootprint = 0.79f / totalFootprints; // 21%～100%を各足跡に割り当てる

        StartCoroutine(LoadFootprintsCoroutine(footprintDataList, ui_Loading, progressPerFootprint));

        //// 足跡データを基に実際の足跡を復元
        //foreach (var footprintData in footprintDataList)
        //{
        //    FootPrintController footprint = Instantiate(footPrintPrefab, footprintData.position, Quaternion.identity);
        //    footprint.LoadFootPrintData(footprintData);

        //    // FootPrintsAllControllerの子オブジェクトに設定
        //    footprint.transform.SetParent(transform);

        //    //ここでスライダーを更新

        //    LoadFootPrints.Add(footprint);
        //}

        //ui_Loading.slider.value = 1.0f; //足跡の復元が終わったらスライダーを1.0に合わせる


        //ui_Loading.ResetLoadingUI();

    }

    private IEnumerator LoadFootprintsCoroutine(List<FootPrintData> footprintDataList, UI_Loading ui_Loading, float progressPerFootprint)
    {
        foreach (var footprintData in footprintDataList)
        {
            // 足跡を復元
            FootPrintController footprint = Instantiate(footPrintPrefab, footprintData.position, Quaternion.identity);
            footprint.LoadFootPrintData(footprintData);

            // FootPrintsAllControllerの子オブジェクトに設定
            footprint.transform.SetParent(transform);

            // 進捗を更新
            ui_Loading.slider.value += progressPerFootprint;

            // 遅延を挟むことでUIの進捗を視覚的に分かりやすく
            yield return null;
        }

        ui_Loading.slider.value = 1.0f; // 完了時にスライダーを最大値に

        // ローディングUIをリセット
        ui_Loading.ResetLoadingUI();
    }
}
