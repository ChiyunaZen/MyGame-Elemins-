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

    public void Bloomflowers()
    {
        // 花をアクティブにして咲かせる処理
        foreach (GameObject flower in spawnedFlowers)
        {
            flower.SetActive(true);  // 花をアクティブにする
        }

        isBlooming = true;  // 花が咲いたフラグを立てる
    }

    //現在の状態を取得して返す
    public FootPrintData GetFootPrintData()
    {
        FootPrintData data = new FootPrintData
        {
            position = transform.position,
            isBlooming = isBlooming,
            flowerPositions = new List<Vector3>(),
            flowerRotations = new List<Quaternion>()
        };

        // 生成された花の位置と回転を保存
        foreach (GameObject flower in spawnedFlowers)
        {
            data.flowerPositions.Add(flower.transform.position);
            data.flowerRotations.Add(flower.transform.rotation);
        }

        return data;
    }

    //ロードして復元
    public void RestoreFootPrint(FootPrintData data)
    {
        transform.position = data.position;
        isBlooming = data.isBlooming;

        
        // 保存されていた花を復元
        for (int i = 0; i < data.flowerPositions.Count; i++)
        {
            Vector3 position = data.flowerPositions[i];
            Quaternion rotation = data.flowerRotations[i];

            GameObject randomFlower = flowers[Random.Range(0, flowers.Length)];
            GameObject spawnedFlower = Instantiate(randomFlower, position, rotation);
            spawnedFlower.transform.parent = transform;

            spawnedFlower.SetActive(data.isBlooming); // 咲いている状態ならアクティブに

            spawnedFlowers.Add(spawnedFlower);
        }
    }
}

