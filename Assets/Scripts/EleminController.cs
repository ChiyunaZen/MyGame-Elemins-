using lilToon;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EleminController : MonoBehaviour
{
    public Material material;
    public float alphaDecreaseAmount = 0.1f; // 透明度を揚げる量

    NavMeshAgent navMeshAgent;
    Animator animator;
    Light eleminLight;

    void Start()
    {

        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        eleminLight = GetComponentInChildren<Light>();
        eleminLight.range = 0;
        eleminLight.intensity = 0;

        material.SetColor("_Color", new Color(1f, 1f, 1f, 0.02f)); //マテリアルを透明よりに設定

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

        // アルファ値を減少させる
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
        //if ( collider.CompareTag("FootLight"))
        //{
        //    Debug.Log("FootLightに触れている");
        //    navMeshAgent.destination = collider.transform.position;
        //    DecreaseTransparency();
        //    Destroy(collider.gameObject); 
        //}

        if (collider.CompareTag("Player"))
        {
            // if (GameObject.FindWithTag("FootLight") == null)
            //{
            //Debug.Log("Playerのほうに行く");
            navMeshAgent.destination = collider.transform.position;
            //}
        }

        //Collider[] colliders = Physics.OverlapSphere(transform.position, 1f); //1f以内のコライダーを取得して配列に格納する
        //GameObject nearistObject = null; //一番近いオブジェクトの変数
        //float closedDistance = Mathf.Infinity; //最初は無限大より小さいものがセットされる

        //foreach (Collider other in colliders)　//配列の中で繰り返す処理
        //{
        //    if (other.CompareTag("FootLight"))
        //    {
        //        float distance = Vector3.Distance(collider.transform.position, other.transform.position);

        //        if (distance < closedDistance)
        //        {
        //            closedDistance = distance;
        //            nearistObject = other.gameObject;
        //        }
        //    }
        //}

        //if (nearistObject != null)
        //{
        //    //一番近いFootLightの位置に移動
        //    navMeshAgent.destination = nearistObject.transform.position;
        //    StartCoroutine(DestroyObjectWhenArrived(nearistObject));
        //}
        //else
        //{
        //    if (collider.CompareTag("Player"))
        //    {
        //        navMeshAgent.destination = collider.transform.position;
        //    }

        //}
    }

    //private IEnumerator DestroyObjectWhenArrived(GameObject target)
    //{
    //    // 移動中
    //    while (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
    //    {
    //        yield return null;
    //    }

    //    // 移動が完了したらオブジェクトを破棄
    //    Destroy(target);
    //}

    public void GoToSymbol(GameObject gameObject)
    {

    }
}
