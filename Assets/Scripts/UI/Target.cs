using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    Vector3 mousePosition;
    Vector3 targetPosition;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (Input .GetMouseButton(0))
        {
            mousePosition = Input.mousePosition;
            mousePosition.z =0;

            targetPosition = Camera.main.ScreenToViewportPoint(mousePosition);

            transform.position = new Vector3(targetPosition.x, targetPosition.y, 3.0f);
        }
        
    }
}
