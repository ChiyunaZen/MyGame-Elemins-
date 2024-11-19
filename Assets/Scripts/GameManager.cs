using Sydewa;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    EleminController eleminController;
    [SerializeField] FootPrintsAllController footPrintsAllController;
    GameObject directionalLight;
    [SerializeField]LightingManager lightingManager;

    [SerializeField] float sunRiseSpeed = 1f;

    private void Awake()
    {
        // directionalLight = GameObject.Find("Directional Light");
    }
    void Start()
    {
        eleminController = GameObject.FindWithTag("SubCharacter").GetComponent<EleminController>();
        lightingManager.TimeOfDay =4 ;

    }
    // Update is called once per frame
    void Update()

    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            footPrintsAllController.GetFootPrintsFlowers();

            StartCoroutine(SunRise());
        }


    }

    IEnumerator  SunRise()
    {
        // 目標の値
        float targetTimeOfDay = 12f;

        // 1秒間に増加する値
        float speed = 1f;

        while (lightingManager.TimeOfDay < targetTimeOfDay)
        {
            // 時刻を徐々に増加
            lightingManager.TimeOfDay += speed * Time.deltaTime;

            // 次のフレームまで待機
            yield return null;
        }

        // 最終的にピッタリ目標時刻にする
        lightingManager.TimeOfDay = targetTimeOfDay;
    }
}


