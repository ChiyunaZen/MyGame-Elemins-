using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    EleminController eleminController;
    GameObject[] footPaints;

    void Start()
    {
        eleminController = GameObject.FindWithTag("SubCharacter").GetComponent<EleminController>();

    }
    // Update is called once per frame
    void Update()

    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape pushed");
            footPaints = GameObject.FindGameObjectsWithTag("FootPrint");

            foreach (GameObject footPrint in footPaints)
            {
                footPrint.GetComponent<FootPrintController>().Bloomflowers();
            }
        }

       
    }
}


