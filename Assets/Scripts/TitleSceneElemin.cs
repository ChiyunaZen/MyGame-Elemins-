using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneElemin : MonoBehaviour
{
    Animator animator;
    public GameObject target;  // ターゲットへの参照
    public float moveSpeed = 1f;        // 移動速度
    public float rotationSpeed = 3f; // 回転速度
    bool isAtTarget = true; // ターゲットに到着したかの判定

    

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        
        Vector3 targetPoint = target.transform.position;

        transform.position = Vector3.Lerp(transform.position, targetPoint, moveSpeed * Time.deltaTime);

        Vector3 lookingPoint = new Vector3(targetPoint.x, targetPoint.y, targetPoint.z - 0.1f);
        transform.LookAt(lookingPoint);

        // 滑らかな回転
        Vector3 direction = targetPoint - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Target"))
        {
            isAtTarget = true;
            animator.SetBool("IsAtTarget",true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Target"))
        {
            isAtTarget= false;
            animator.SetBool("IsAtTarget",false);
        }
    }
}


