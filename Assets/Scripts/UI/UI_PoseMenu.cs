using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PoseMenu : MonoBehaviour
{
    Canvas poseCanvas;

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
            isPosing = true;
        }
        else
        {
            if (!UI_MenuController.Instance.isOptionmenu)
                ExitPoseMenu();
        }
    }
}
