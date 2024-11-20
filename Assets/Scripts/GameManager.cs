using Sydewa;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    EleminController eleminController;
    [SerializeField] FootPrintsAllController footPrintsAllController;
    GameObject directionalLight;
    [SerializeField] LightingManager lightingManager;

    [SerializeField] float startTimeOfDay = 2;
    [SerializeField] float targetTimeOfDay = 12f;
    [SerializeField] float sunRiseSpeed = 1f;
    [SerializeField] float startBloomSunTime = 6f;
    GameObject Enemy;




    private void Awake()
    {
        // directionalLight = GameObject.Find("Directional Light");
    }
    void Start()
    {
        eleminController = GameObject.FindWithTag("SubCharacter").GetComponent<EleminController>();
        lightingManager.TimeOfDay = startTimeOfDay;
        Enemy = GameObject.FindWithTag("Enemy");

    }
    // Update is called once per frame
    void Update()

    {


    }

    public void Ending()
    {
        if (Enemy != null)
        {
            Destroy(Enemy);
        }


        StartCoroutine(SunRise());

    }

    IEnumerator SunRise()
    {

        while (lightingManager.TimeOfDay < targetTimeOfDay)
        {
            // 時刻を徐々に増加
            lightingManager.TimeOfDay += sunRiseSpeed * Time.deltaTime;

            //設定時刻になったら花を咲かせるメソッドを呼び出す
            if (lightingManager.TimeOfDay >= startBloomSunTime)
            {
                footPrintsAllController.GetFootPrintsFlowers();
            }

            // 次のフレームまで待機
            yield return null;
        }

        // 最終的に目標時刻にそろえる
        lightingManager.TimeOfDay = targetTimeOfDay;


    }
}


