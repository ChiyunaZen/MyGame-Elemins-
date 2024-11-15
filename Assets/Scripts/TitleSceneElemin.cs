using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneElemin : MonoBehaviour
{
    Animator animator;
    public GameObject target;  // ターゲットへの参照
    public float speed;        // 移動速度
    public bool isTouch;       // ターゲットに接触しているかどうか

    void Start()
    {
        speed = 0.1f;  // 移動速度の初期設定
        target = GameObject.Find("Target");  // ターゲットの参照を取得
        isTouch = false;  // 初期状態では接触していない
    }

    // 衝突時に呼ばれるメソッド
    private void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトがターゲットだった場合
        if (collision.gameObject.name == "Target")
        {
            isTouch = true;  // 接触フラグを立てる
        }
    }

    void Update()
    {
        // ターゲットに接触していない場合は移動
        if (isTouch == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
        }
    }
}

