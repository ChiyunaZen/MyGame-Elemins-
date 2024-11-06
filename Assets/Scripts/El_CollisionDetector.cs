using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class El_CollisionDetector : MonoBehaviour
{
    EleminController elaminController;

    void Start()
    {
        elaminController = GetComponentInParent<EleminController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
            elaminController.OnDetectObject(other);
        
    }
}
