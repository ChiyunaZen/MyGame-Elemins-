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
        footPaints = GameObject.FindGameObjectsWithTag("FootPrint");
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Escape) )
        {
           foreach ( GameObject footPrint in footPaints )
            {
                footPrint.GetComponent<FootPrintController>().Bloomflowers();
            }
        }
    }
}
