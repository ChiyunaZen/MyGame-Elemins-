using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrintData 
{
    public Vector3 position;    // 足跡の位置
    public bool isBlooming;     // 花が咲いているかどうか
    public List<Vector3> flowerPositions; // 生成した花の位置
    public List<Quaternion> flowerRotations; // 生成した花の回転
}
