using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    EleminController eleminController;
    [SerializeField] FootPrintsAllController footPrintsAllController;
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
            footPrintsAllController.GetFootPrintsFlowers();
        }

      
    }
}


