using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData 
{
    public string sceneName; //現在のシーン名
    public Vector3 playerPos; //プレイヤーの位置

    public EleminData eleminData; //Eleminのデータ

    public List<FootPrintData> footPrints; // 足跡リストのデータ

    public List<SymbolData> symbols;　//シンボルリストのデータ

    public float gameTime;　//ゲーム内の時間
}


