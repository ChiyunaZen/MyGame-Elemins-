using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrintLight : MonoBehaviour
{
    public float fadeDirection = 2.0f; //光のフェードアウトにかかる時間
    private Light footLight;
    private float startTime;

    void Start()
    {
        footLight = GetComponent<Light> ();
        startTime = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        float eleapsed = Time.time - startTime;

        if (eleapsed < fadeDirection)
        {
            Color color = footLight.color;
            color.a = Mathf.Lerp(1, 0, eleapsed / fadeDirection);
           footLight.color = color;
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
