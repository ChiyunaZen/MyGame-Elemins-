using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AllSymbolManager : MonoBehaviour
{
    public List<SymbolController> symbols;

    void Awake()
    {
        
    }
    private void Start()
    {
        // シーン内のすべてのSymbolControllerを取得
        symbols.AddRange(FindObjectsOfType<SymbolController>());
    }

    public List<SymbolData> GetSymbolDataList()
    {
        List<SymbolData> symbolDataList = new List<SymbolData>();
        foreach (var symbol in symbols)
        {
            symbolDataList.Add(symbol.GetSymbolData());
        }
        return symbolDataList;
    }


    public void LoadSymbolDataList(List<SymbolData> symbolDataList)
    {
        foreach (var symbolData in symbolDataList)
        {
            // symbolIdで対応するSymbolControllerを検索してロード
            SymbolController symbol = symbols.Find(s => s.symbolId == symbolData.symbolId);
            if (symbol != null)
            {
                symbol.LoadSymbolData(symbolData);
            }
            else
            {
                Debug.LogWarning($"シンボルが見つかりません: symbolId = {symbolData.symbolId}");
            }
        }
    }

    
}
