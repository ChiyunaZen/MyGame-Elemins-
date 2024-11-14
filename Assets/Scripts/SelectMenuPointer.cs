using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMenuPointer : MonoBehaviour
{
    public Light pointerLight;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
               Instantiate(pointerLight,hit.point,Quaternion.identity);
            }
        }
    }

}

