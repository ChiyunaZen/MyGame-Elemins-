using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class ElaminController : MonoBehaviour
{
   
    NavMeshAgent navMeshAgent;
    Animator animator;

    void Start()
    {
      
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
        animator.SetFloat("Speed",navMeshAgent.velocity.magnitude);

    }

    public void OnDetectObject(Collider collider)
    {
            navMeshAgent.destination = collider.transform.position;
       
    }
}
