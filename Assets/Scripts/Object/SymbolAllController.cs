using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SymbolAllController : MonoBehaviour,ISceneLoadCheck
{
    public List<SymbolController> symbols;

    public List<SymbolData> symbolsDataList;
    //public bool isLoadAllSymbol;

    public bool isSymbolAllActive;

    // シーン遷移時にシンボルを再取得する
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

   

    void Awake()
    {
        // すでにシンボルリストが空でない場合は再取得しない
        if (symbols.Count == 0)
        {
            // シーン内のすべてのSymbolControllerを取得
            symbols.AddRange(FindObjectsOfType<SymbolController>());
        }

    }
    private void Start()
    {
        // シーン内のすべてのSymbolControllerを取得
        // もしAwakeで取得していなかった場合に追加する処理
        if (symbols.Count == 0)
        {
            symbols.AddRange(FindObjectsOfType<SymbolController>());
        }

        isSymbolAllActive = true;
      
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
        // シンボルリストをクリア
        symbols.Clear();

        // 新たにシーン内のすべてのSymbolControllerを取得してリストに追加
        symbols.AddRange(FindObjectsOfType<SymbolController>());

        // シンボルリストに対して symbolDataList のデータを適用
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

    // シーンがロードされたときに呼ばれる
    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        // シンボルリストをクリアして、新たにシーン内のすべてのSymbolControllerを取得
        symbols.Clear();
        symbols.AddRange(FindObjectsOfType<SymbolController>());

        // シンボルデータをロード
        if (symbolsDataList != null && symbolsDataList.Count > 0)
        {
            LoadSymbolDataList(symbolsDataList);
        }
    }

    public bool IsReady()
    {
        return isSymbolAllActive;
    }
}
