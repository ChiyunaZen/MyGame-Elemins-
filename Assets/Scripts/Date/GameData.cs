using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//Jsonファイルとして書き出して保存するためのデータクラス
public class GameData 
{
    public string sceneName; //現在のシーン名
    public Vector3 playerPos; //プレイヤーの位置

    public EleminData eleminData; //Eleminのデータ

    public List<FootPrintData> footPrints; // 足跡リストのデータ

    public List<SymbolData> symbols;　//シンボルリストのデータ

    public float gameTime;　//ゲーム内の時間
}

//それぞれ設定を保存したいデータのクラス

//Eleminのデータクラス
[System.Serializable]
public class EleminData
{
    public Vector3 eleminPos;　//Eleminの現在位置
    public float eleminAlpha;　//Eleminの現在のマテリアルカラーのアルファ値
    public float eleminRange; //Eleminに着けているのライトのRangeの値
    public float eleminIntensity;　//Eleminに着けているライトのIntensityの値
}

//シンボルのデータ
[System.Serializable]
public class SymbolData
{
    public int symbolId; //シンボルのID　
    public float symbolLightRange; //シンボルライトのRange値
    public float symbolLightIntensity;　//シンボルのライトのIntensity値
    public bool isLighting; //ライトがついているかのフラグ

}

[System.Serializable]
//足跡のデータ
public class FootPrintData
{
    public Vector3 position;    // 足跡の位置
    public bool isBlooming;     // 花が咲いているかどうか
    public List<Vector3> flowerPositions; // 生成した花の位置
    public List<Quaternion> flowerRotations; // 生成した花の回転
}

