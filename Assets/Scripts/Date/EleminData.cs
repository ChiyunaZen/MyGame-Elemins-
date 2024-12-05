using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EleminData
{


    //セーブ、ロードしたいデータ
    public Vector3 eleminPos { get; set; }　//Eleminの現在位置
    public float eleminAlpha { get; set; }　//Eleminの現在のマテリアルカラーのアルファ値
    public float eleminRange { get; set; } //Eleminに着けているのライトのRangeの値
    public float eleminIntensity { get; set; }　//Eleminに着けているライトのIntensityの値


}
