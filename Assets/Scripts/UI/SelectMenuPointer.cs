using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMenuPointer : MonoBehaviour
{
    public bool IsClicked {  get; private set; }
    Animator animator;
    public AudioClip clip;
    AudioSource source;

    UI_MenuController menuController;

    public GameObject target; //Eleminを移動させるためのターゲットオブジェクト

    void Start()
    {
        menuController = GameObject.FindAnyObjectByType<UI_MenuController>();
        animator = GetComponent<Animator>();

        source = GetComponent<AudioSource>();
        source.clip = clip;
        IsClicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!menuController.isOptionmenu&&!GameManager.Instance.IsOpenExitDialog)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;



            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (!IsClicked)
                    {
                        IsClicked = true;
                        animator.SetBool("isClicked", true);
                        source.Play();
                    }
                    transform.LookAt(hit.point);

                    target.transform.position = hit.point;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (IsClicked)
                {
                    IsClicked = false;
                    animator.SetBool("isClicked", false);
                }
            }
        }
    }

}

