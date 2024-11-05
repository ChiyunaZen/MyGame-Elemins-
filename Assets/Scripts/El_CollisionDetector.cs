using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class El_CollisionDetector : MonoBehaviour
{
    ElaminController elaminController;

    void Start()
    {
        elaminController = GetComponentInParent<ElaminController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            elaminController.OnDetectObject(other);
        }
    }
}
