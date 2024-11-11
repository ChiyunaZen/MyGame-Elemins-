using lilToon;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EleminController : MonoBehaviour
{
    public Material material;
    public float alphaDecreaseAmount = 0.05f; // 透明度をあげる量

    NavMeshAgent navMeshAgent;
    Animator animator;
    Light eleminLight;
    Transform playerTransform;

    bool isNearSymbol = false; //近くにシンボルが存在するかのフラグ

    



    void Start()
    {

        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        eleminLight = GetComponentInChildren<Light>();
        eleminLight.range = 0;
        eleminLight.intensity = 0;

        material.SetColor("_Color", new Color(1f, 1f, 1f, 0.0f)); //マテリアルを透明に設定
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Playerを見つけて設定
    }

    // Update is called once per frame
    void Update()
    {

        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);


    }

    // 透明度を上げるメソッド
    public void DecreaseTransparency()
    {
        // 現在の色を取得
        Color currentColor = material.GetColor("_Color");

        // アルファ値を増加させる
        float newAlpha = Mathf.Clamp(currentColor.a + alphaDecreaseAmount, 0f, 1f); // 0未満にならないように制限
        currentColor.a = newAlpha; // 新しいアルファ値を設定

        // マテリアルに新しい色を設定
        material.SetColor("_Color", currentColor);

        if (currentColor.a <= 0.9f)
        {
            eleminLight.range = currentColor.a;
        }
        else
        {
            eleminLight.range += 0.1f;

            if (eleminLight.intensity <= 3.5f)
            {
                eleminLight.intensity += 0.1f;
            }
        }
    }




    public void OnDetectObject(Collider collider)
    {

        if (collider.CompareTag("Player") && !isNearSymbol)
        {
            navMeshAgent.destination = playerTransform.position;
        }

    }

    public void GoToSymbol(GameObject symbolObject)
    {

        //NavMeshのターゲット(移動先)をシンボルに変更
        navMeshAgent.destination = symbolObject.transform.position;
        isNearSymbol = true;

        // シンボルに到達するまで確認するコルーチンを開始
        StartCoroutine(MoveToSymbolAndReturn(symbolObject));
    }


    // シンボルへ移動し、光を分けた後プレイヤーに戻るコルーチン
    private IEnumerator MoveToSymbolAndReturn(GameObject symbolObject)
    {


        // シンボルに向かって移動
        while (Vector3.Distance(transform.position, symbolObject.transform.position) > 0.5f)
        {
            yield return null; // シンボルに到達するまで待機
        }


        SymbolController symbolController = symbolObject.GetComponent<SymbolController>();
        if (symbolController != null)
        {
            symbolController.ActivateSymbolLight();

            // SymbolControllerで設定された範囲と強度を取得して光を減少
            float decreaseRange = symbolController.getLightRange;
            float decreaseIntensity = symbolController.getLightIntensity;

            // 光の範囲と強度を一度だけ減少
            DecreaseLightRange(decreaseRange);
            DecreaseLightIntensity(decreaseIntensity);

            Collider collider = symbolObject.GetComponent<Collider>();
            Destroy(collider);
        }




        yield return new WaitForSeconds(1.5f);


        // ターゲットをプレイヤーに戻す
        navMeshAgent.destination = playerTransform.position;
        isNearSymbol = false;

    }


    //Eleminライトの範囲を減らすメソッド
    public void DecreaseLightRange(float value)
    {
        //if (eleminLight.range - value >= 1)
        //{
        //    eleminLight.range -= value;
        //}
        //else
        //{
        //    eleminLight.range = 1f;
        //}

        eleminLight.range = Mathf.Max(eleminLight.range - value, 0f); // 最小値を1にする
    }

    //Eleminライトの明るさを減らすメソッド
    public void DecreaseLightIntensity(float value)
    {
        //if (eleminLight.intensity - value >= 0.1)
        //{
        //    eleminLight.intensity -= value;
        //}
        //else
        //{
        //    eleminLight.intensity = 0.1f;

        //}

        eleminLight.intensity = Mathf.Max(eleminLight.intensity - value, 0f); // 最小値を0.1にする
    }

    public void StartPlayerTarget()
    {
        navMeshAgent.destination = playerTransform.position;

    }
}