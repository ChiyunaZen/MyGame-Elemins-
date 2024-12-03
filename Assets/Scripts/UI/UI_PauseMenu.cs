using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PauseMenu : MonoBehaviour
{
    Canvas poseCanvas;
    [SerializeField] UI_MenuController menuController;

    public bool isPosing = false; //ポーズ中かのフラグ
    void Start()
    {
        poseCanvas = GetComponentInParent<Canvas>();
        poseCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (!isPosing)
        //    {
        //        poseCanvas.enabled = true;
        //        isPosing =true;
        //    }
        //    else
        //    {
        //        if(!UI_MenuController.Instance.isOptionmenu)
        //       ExitPoseMenu();
        //    }
        //}
    }


   public void ExitPoseMenu()
    {
        poseCanvas.enabled = false;
        Time.timeScale = 1;
        isPosing = false;
    }

    public void ExitButton()
    {
        GameManager.Instance.ShowExitDialog();
    }

    public void ToggleShowPose()
    {
        if (!isPosing)
        {
            poseCanvas.enabled = true;
            Time.timeScale = 0;
            isPosing = true;
        }
        else
        {
            if (!menuController.isOptionmenu)
                ExitPoseMenu();
        }
    }
  
}
