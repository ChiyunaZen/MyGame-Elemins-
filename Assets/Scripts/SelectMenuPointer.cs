using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMenuPointer : MonoBehaviour
{
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
              transform.LookAt(hit.point);
            }
        }
    }

}

