using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class ElaminController : MonoBehaviour
{
    PlayerController _playerCon;
    NavMeshAgent navMeshAgent;
    void Start()
    {
        _playerCon = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = _playerCon.transform.position;
        

    }
}
