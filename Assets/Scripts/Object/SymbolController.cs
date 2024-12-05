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

    //ライトの明るさに乗算する値の調整用(受け取る量に対してどの程度増やすか)
    [SerializeField] private float intensityMultiplier = 10f; 
    [SerializeField] private float rangeMultiplier = 3f;

    private GameObject currentLightInstance;
    private Light pointLight;

    private static int globalSymbolIdCounter = 0; // ID振り分けようの静的カウンター
    public int symbolId { get; private set; } // 各シンボルのID

    Collider symbolcollider;

    private void Awake()
    {
        //IDを生成して割り振る
        symbolId = globalSymbolIdCounter++;
    }

    void Start()
    {

        symbolcollider = GetComponent<Collider>();

        //if (isSymbolLigting) //もしすでに点灯済ならコライダーは非アクティブにする
        //{
        //    symbolcollider.enabled = false;
        //}

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

        symbolcollider.enabled = false;

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
            pointLight.intensity = Mathf.MoveTowards(pointLight.intensity, getLightIntensity * intensityMultiplier, lightIncreaseSpeed * Time.deltaTime);
            pointLight.range = Mathf.MoveTowards(pointLight.range, getLightRange * rangeMultiplier, lightIncreaseSpeed * Time.deltaTime);



            yield return null; // 次のフレームまで待機
        }
    }

    public SymbolData GetSymbolData()
    {
        //セーブ用のデータを保存する
        SymbolData data = new SymbolData();
        data.symbolId = symbolId;

        if (pointLight != null)
        {
            data.symbolLightRange = pointLight.range;
            data.symbolLightIntensity = pointLight.intensity;
        }
        else
        {
            data.symbolLightRange = 0f;
            data.symbolLightIntensity = 0f;
        }

        data.isLighting = isSymbolLigting;

        return data;

    }

    public void LoadSymbolData(SymbolData symbolData)
    {
        //ロードする
        if (symbolData != null)
        {
            isSymbolLigting = symbolData.isLighting;
            
            if (isSymbolLigting)
            {
                ActivateSymbolLight();
                if (pointLight != null)
                {
                    pointLight.range = symbolData.symbolLightRange;
                    pointLight.intensity = symbolData.symbolLightIntensity;
                }
            }
        }
        else
        {
            Debug.LogWarning("ロードするデータが見つかりません。");
        }
    }
}

