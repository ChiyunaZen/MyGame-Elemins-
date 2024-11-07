using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    EleminController eleminController;
    GameObject[] footPaints;
    GameObject directionalLight;

    private void Awake()
    {
       // directionalLight = GameObject.Find("Directional Light");
    }
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
           // directionalLight.SetActive(true);

            foreach (GameObject footPrint in footPaints)
            {
                footPrint.GetComponent<FootPrintController>().Bloomflowers();
            }
        }


    }
}


