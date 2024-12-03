using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MenuController : MonoBehaviour
{
    //public static UI_MenuController Instance { get; private set; }
    public bool isOptionmenu {  get; private set; }

    private CanvasGroup canvasGroup;
    [SerializeField] private CanvasGroup topCanvasGroup;
    [SerializeField] private CanvasGroup soundCanvasGroup;

   // private bool isMenuOpened; //メニュー画面が開いているかのフラグ
    private bool isTopMenu; //現在TOPメニューの表示かのフラグ

    Animator animator;

    private void Awake()
    {
        //if (Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject); // シーンをまたいでも破棄されないようにする
        //}
        //else
        //{
        //    Destroy(gameObject); // 二重に存在する場合は破棄
        //}
    }

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;

        animator = GetComponent<Animator>();

        isOptionmenu = false;
        topCanvasGroup.interactable = false;
        soundCanvasGroup.interactable = false;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
             
            if (!isOptionmenu)
            {
                return;
            }
            else
            {
                if (isTopMenu)
                {
                    CloseButton();
                }
                else
                {
                    SoundMenuClose();
                }
            }
            
        }

    }

    public void OpenMenu()
    {
        animator.SetTrigger("OpenMenu");
        canvasGroup.blocksRaycasts = true;
        isOptionmenu = true;
        isTopMenu = true;
        topCanvasGroup.interactable = true;
    }

    public void CloseButton()
    {
        animator.SetTrigger("CloseMenu");
        canvasGroup.blocksRaycasts = false;
        isOptionmenu = false;
        topCanvasGroup.interactable = false;
    }

    public void SoundMenuOpen()
    {
        if (isTopMenu)
        {
            animator.SetTrigger("OpenSoundMenu");
            isTopMenu = false;
        }
        topCanvasGroup.interactable = false;　//Topメニューの操作を不可に
        soundCanvasGroup.interactable = true; //サウンドメニューを操作可能に
    }

    public void SoundMenuClose()
    {
        if (!isTopMenu)
        {
            animator.SetTrigger("CloseSoundMenu");
            isTopMenu = true;
        }
        topCanvasGroup.interactable = true;
        soundCanvasGroup.interactable = false;
    }

}
