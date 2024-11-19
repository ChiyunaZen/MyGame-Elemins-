using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrintController : MonoBehaviour
{
   [SerializeField] GameObject[] flowers;
    public int minFlower = 2; //花の最小値
    public int maxFlower = 4; //花の最大値
    public float bloomRadius = 0.5f; //花が発生する範囲円の半径
    public bool isBlooming = false;

    private List<GameObject> spawnedFlowers = new List<GameObject>();  // 生成した花を保持するリスト

    void Start()
    {
        int flowerCount = Random.Range(minFlower, maxFlower + 1);

        for (int i = 0; i < flowerCount; i++)
        {
            GameObject randomFlower = flowers[Random.Range(0, flowers.Length)];

            //ランダムな位置を決定
            Vector3 randomPosition = transform.position + (Random.insideUnitSphere * bloomRadius);
            randomPosition.y = transform.position.y; // Y座標を固定

            //Y軸のみランダム回転させる
            float randomYRotation = Random.Range(0, 360f);
            Quaternion randamRotation = Quaternion.Euler(0f, randomYRotation, 0f);

            // 花のプレハブをランダムな位置に生成
            GameObject spawnedFlower = Instantiate(randomFlower, randomPosition, randamRotation);
            spawnedFlower.transform.parent = transform; // このオブジェクトの子として配置

            spawnedFlower.SetActive(false);

            spawnedFlowers.Add(spawnedFlower);  // 生成した花をリストに追加
        }

        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Bloomflowers()
    {
        // 花をアクティブにして咲かせる処理
        foreach (GameObject flower in spawnedFlowers)
        {
            flower.SetActive(true);  // 花をアクティブにする
        }

        isBlooming = true;  // 花が咲いたフラグを立てる
    }
}

