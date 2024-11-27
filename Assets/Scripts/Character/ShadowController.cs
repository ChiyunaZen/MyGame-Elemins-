using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShadowController : MonoBehaviour, IFollowMov
{
    Transform eleminTransform;
    NavMeshAgent navMeshAgent;
    EleminController eleminController;

    public float getLightIntensity = 0.2f; // Eleminから奪う光量
    public float getLightRange = 2f;　// Eleminから奪う照らす範囲
   // public float navMeshSpeed = 1.5f; //NavMeshの追跡速度

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent != null)
        {
            //navMeshAgent.obstacleAvoidanceType = NavMeshObstacleAvoidanceType.None; // 衝突回避を無効化
            navMeshAgent.avoidancePriority = 0; // 他のエージェントと衝突しないように優先度を最小に設定
           // navMeshAgent.moveSpeed = navMeshSpeed;
        }

        eleminTransform = GameObject.FindGameObjectWithTag("SubCharacter").transform; // elemminを見つけて設定
        eleminController = GameObject.FindGameObjectWithTag("SubCharacter").GetComponent<EleminController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartFollowing()
    {
        navMeshAgent.destination = eleminTransform.position;
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + "と接触しました");

        if (other.CompareTag("SubCharacter"))  // Eleminがシャドウのコライダー内に入ったとき
        {

            // Eleminを制止させる
            eleminController.StopFollowing();
            eleminController.DecreaseLightRange(getLightRange);
            eleminController.DecreaseLightIntensity(getLightIntensity);


            //自身も制止する
            StopFollowing();
        }
    }

    //void OnTriggerExit(Collider other)
    //{

    //    if (other.CompareTag("SubCharacter"))
    //    {
    //        other.GetComponent<NavMeshAgent>().isStopped = false;
    //    }
    //}




    public void StopFollowing()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed -= 0.2f;
        StartCoroutine(RestartFollowing());
    }

    public IEnumerator RestartFollowing()
    {
        yield return new WaitForSeconds(8);
        navMeshAgent.isStopped = false;
    }


}
