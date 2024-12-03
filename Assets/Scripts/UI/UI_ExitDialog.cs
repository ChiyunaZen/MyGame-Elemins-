using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_ExitDialog : MonoBehaviour
{
    [SerializeField] private GameObject exitDialog;



    public void Close()
    {
       // exitDialog.SetActive(false);
    }

    public void OnYesExit()
    {
        // GameManager の ExitGame メソッドを呼び出す
        GameManager.Instance.ExitGame();
    }

    public void OnNoExit()
    {
      GameManager.Instance.CancelExitGame();
    }
}
