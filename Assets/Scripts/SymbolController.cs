using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolController : MonoBehaviour
{
    [SerializeField] GameObject symbolLightPrefab;
    public float getLightIntensity = 1f; // Eleminから受け取る光量
    public float getLightRange = 5f;　// Eleminから受け取る照らす範囲
    public float lightIncreaseSpeed = 0.5f; // 光の増加速度
    public bool isSymbolLigting = false; //シンボルが点灯済かのフラグ

    private GameObject currentLightInstance;
    private Light pointLight;

    Collider symbolcollider;


    void Start()
    {

        symbolcollider = GetComponent<Collider>();

        if (isSymbolLigting) //もしすでに点灯済ならコライダーは非アクティブにする
        {
            symbolcollider.enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    // シンボルライトをアクティベートするメソッド
    public void ActivateSymbolLight()
    {
        // 既にライトが生成されている場合は何もしない
        if (isSymbolLigting) return;

        Vector3 lightPos = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        // symbolLightPrefabをこのオブジェクトの子オブジェクトとして生成
        currentLightInstance = Instantiate(symbolLightPrefab, lightPos, Quaternion.identity, transform);

        // インスタンス化されたオブジェクトからポイントライトを取得
        pointLight = currentLightInstance.GetComponent<Light>();

        if (pointLight != null)
        {
            // IntensityとRangeを0から開始
            pointLight.intensity = 0;
            pointLight.range = 0;

            // 光が徐々に増えるようにする
            StartCoroutine(IncreaseLightOverTime());
            isSymbolLigting = true;
            symbolcollider.enabled = false;　//コライダーは非アクティブに
        }
        else
        {
            Debug.LogWarning("symbolLightPrefabにLightコンポーネントがありません。");
        }
    }

    // コルーチンでライトのIntensityとRangeを増加
    private IEnumerator IncreaseLightOverTime()
    {
        while (pointLight.intensity < getLightIntensity || pointLight.range < getLightRange)
        {
            // IntensityとRangeを徐々に増加
            pointLight.intensity = Mathf.MoveTowards(pointLight.intensity, getLightIntensity * 10, lightIncreaseSpeed * Time.deltaTime);
            pointLight.range = Mathf.MoveTowards(pointLight.range, getLightRange * 3, lightIncreaseSpeed * Time.deltaTime);



            yield return null; // 次のフレームまで待機
        }
    }
}

