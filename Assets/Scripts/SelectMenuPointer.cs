using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMenuPointer : MonoBehaviour
{
    Animator animator;
    bool isClicked = false;
   public AudioClip clip;
    AudioSource source;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        
        source = GetComponent<AudioSource>();
        source.clip = clip;
    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;



        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (!isClicked)
                {
                    isClicked = true;
                    animator.SetBool("isClicked", true);
                    source.Play();
                }
              transform.LookAt(hit.point);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if(isClicked)
            {
                isClicked= false;
                animator.SetBool("isClicked",false);
            }
        }
    }

}

