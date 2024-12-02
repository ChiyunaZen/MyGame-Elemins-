using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ExitDialog : MonoBehaviour
{
    [SerializeField] private GameObject exitDialog;

    private void Start()
    {
        //exitDialog.SetActive(false);
    }

    public void Show()
    {
       // exitDialog.SetActive(true);
    }

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
