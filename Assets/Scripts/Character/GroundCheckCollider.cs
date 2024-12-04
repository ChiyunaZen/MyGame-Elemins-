using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckCollider : MonoBehaviour
{
    public bool isFrontGround;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

     void OnTriggerEnter(Collider other)
    {
        if (!isFrontGround)
        {
            isFrontGround = true;
            Debug.Log("正面は地面です");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isFrontGround)
        {
            isFrontGround = false;
            Debug.Log("正面は崖です");
        }
    }
}
