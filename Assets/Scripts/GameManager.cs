using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    EleminController eleminController;
    GameObject[] footPrints;
    GameObject directionalLight;

    private void Awake()
    {
        // directionalLight = GameObject.Find("Directional Light");
    }
    void Start()
    {
        eleminController = GameObject.FindWithTag("SubCharacter").GetComponent<EleminController>();

    }
    // Update is called once per frame
    void Update()

    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape pushed");
            footPrints = GameObject.FindGameObjectsWithTag("FootPrint");
            // directionalLight.SetActive(true);

            // Coroutineで少しずつ花を生成
            StartCoroutine(BloomFlowersInSequence());


        }

        IEnumerator BloomFlowersInSequence()
        {
            // 逆順でfootPrintsを処理
            for (int i = footPrints.Length - 1; i >= 0; i--)
            {
                // FootPrintControllerを取得して花を咲かせる
                FootPrintController footPrintController = footPrints[i].GetComponent<FootPrintController>();
                footPrintController.Bloomflowers();

                // 次のプレハブの花を生成するまで遅延を入れる
                yield return new WaitForSeconds(0.05f);  // 0.1秒の遅延（適宜調整可能）
            }
        }
    }
}


