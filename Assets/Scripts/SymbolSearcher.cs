using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolSearcher : MonoBehaviour
{
    EleminController eleminController;
    void Start()
    {
        eleminController = GetComponentInParent<EleminController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Symbol"))
        {
            var symbol = other.gameObject;
            eleminController.GoToSymbol(symbol);
        }
    }
}
