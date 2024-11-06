using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrintController : MonoBehaviour
{
   [SerializeField] GameObject[] flowers;
    public int minFlower = 2; //花の最小値
    public int maxFlower = 4; //花の最大値
    public float bloomRadius = 0.5f; //花が発生する範囲円の半径

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Bloomflowers()
    {
        int flowerCount = Random.Range(minFlower, maxFlower + 1);

        for (int i = 0; i < flowerCount; i++)
        {
            GameObject randomFlower = flowers[Random.Range(0,flowers.Length)];

            //ランダムな位置を決定
            Vector3 randomPosition = transform.position + (Random.insideUnitSphere * bloomRadius);
            randomPosition.y = transform.position.y+0.1f ; // Y座標を固定

            // 選ばれた花のプレハブをランダムな位置に生成し、親オブジェクトとしてこのオブジェクトを設定
            GameObject spawnedFlower = Instantiate(randomFlower, randomPosition, Quaternion.identity);
            spawnedFlower.transform.parent = transform; // このオブジェクトの子として配置
        }
    }
}
