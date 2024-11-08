using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolSearcher : MonoBehaviour
{
    EleminController eleminController;
    //public float range = 5f; //減らす範囲の値
    //public float intensity = 1f;　//減らす光量の値

    void Start()
    {
        eleminController = GetComponentInParent<EleminController>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightSymbol"))
        {
            var symbol = other.gameObject;
            eleminController.GoToSymbol(symbol);
        }
    }
}
