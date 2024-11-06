using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrintLight : MonoBehaviour
{
    public float fadeDirection = 2.0f; //光のフェードアウトにかかる時間
    private Light footLight;
    private float startTime;
    Animator animator;

    void Start()
    {
        footLight = GetComponent<Light>();
        animator = GetComponent<Animator>();

        StartCoroutine(LightEnd());

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator LightEnd()
    {
        yield return new WaitForSeconds(fadeDirection);
        animator.SetTrigger("FootLightEnd");

        StartCoroutine(LightDestroy(1));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("eating"))
        {
            other.GetComponentInParent<EleminController>().DecreaseTransparency();
            animator.SetTrigger("FootLightEaten");
            StartCoroutine (LightDestroy(0.5f));

        }
    }

    IEnumerator LightDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

}
