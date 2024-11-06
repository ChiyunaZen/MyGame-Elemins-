using lilToon;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class ElaminController : MonoBehaviour
{
    public Material material;
    public float transparencyDecreaseAmount = 0.1f; // 透明度を揚げる量

    NavMeshAgent navMeshAgent;
    Animator animator;

    void Start()
    {

        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();


        material.SetColor("_Color", new Color(1f, 1f, 1f, 0.1f)); //マテリアルを透明よりに設定

    }

    // Update is called once per frame
    void Update()
    {

        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

        if (Input.GetKeyDown(KeyCode.Space))
        {

            DecreaseTransparency();
        }
    }

    // 透明度を上げるメソッド
    private void DecreaseTransparency()
    {
        // 現在の色を取得
        Color currentColor = material.GetColor("_Color");

        // アルファ値を減少させる
        float newAlpha = Mathf.Clamp(currentColor.a + transparencyDecreaseAmount, 0f, 1f); // 0未満にならないように制限
        currentColor.a = newAlpha; // 新しいアルファ値を設定

        // マテリアルに新しい色を設定
        material.SetColor("_Color", currentColor);
    }




    public void OnDetectObject(Collider collider)
    {
        if ( collider.CompareTag("FootLight"))
        {

            navMeshAgent.destination = collider.transform.position;
        }

        if (collider.CompareTag("Player"))
        {
        navMeshAgent.destination = collider.transform.position;
        }
    }
}
