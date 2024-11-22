using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrintLight : MonoBehaviour
{
    public float fadeDirection = 2.0f; //光のフェードアウトにかかる時間
    private Light footLight;
    private float startTime;
    Animator animator;
    ParticleSystem particle;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        footLight = GetComponent<Light>();
    }
    void Start()
    {
        
        StartCoroutine(LightEnd());
        particle = GetComponentInChildren<ParticleSystem>();
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
            if (other.GetComponentInParent<EleminController>() == null) Debug.Log("EleminContorollerが見つかりません");

            if (animator == null) Debug.Log("Animatorが見つかりません");

            

                other.GetComponentInParent<EleminController>().DecreaseTransparency();

                animator.SetTrigger("FootLightEaten");
                StartCoroutine(LightDestroy(0.51f));
            

        }
    }

    void PlayParticle()
    {
        particle.Play();
    }

    IEnumerator LightDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }



}
