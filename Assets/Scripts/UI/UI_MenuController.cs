using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MenuController : MonoBehaviour
{
   // private CanvasGroup allcanvasGroup;
    [SerializeField] private CanvasGroup topCanvasGroup;
    [SerializeField] private CanvasGroup soundCanvasGroup;

    private bool isMenuOpened; //メニュー画面が開いているかのフラグ
    private bool isTopMenu; //現在TOPメニューの表示かのフラグ

    Animator animator;

    void Start()
    {
      //  allcanvasGroup = GetComponent<CanvasGroup>();
        
        animator = GetComponent<Animator>();

        isMenuOpened = false;
        topCanvasGroup.interactable = false;
        soundCanvasGroup.interactable = false;
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape)&&!isMenuOpened)
        {
            OpenMenu();
        }
    }

    public void OpenMenu()
    {
        animator.SetTrigger("OpenMenu");
        isMenuOpened = true;
        isTopMenu = true;
        topCanvasGroup.interactable= true;
    }

   public void CloseButton()
    {
            animator.SetTrigger("CloseMenu");
            isMenuOpened = false;
            topCanvasGroup.interactable= false;
    }

    public void SoundMenuOpen()
    {
        if (isTopMenu)
        {
            animator.SetTrigger ("OpenSoundMenu");
            isTopMenu = false;
        }
        topCanvasGroup.interactable=false;　//Topメニューの操作を不可に
        soundCanvasGroup.interactable=true; //サウンドメニューを操作可能に
    }

    public void SoundMenuClose()
    {
        if (!isTopMenu)
        {
            animator.SetTrigger("CloseSoundMenu");
            isTopMenu = true;
        }
        topCanvasGroup.interactable=true;
        soundCanvasGroup.interactable=false;
    }

}
