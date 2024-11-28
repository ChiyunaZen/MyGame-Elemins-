using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ExitDialog : MonoBehaviour
{
    public void OnYesButtonClicked()
    {
        // GameManager の ExitGame メソッドを呼び出す
        GameManager.Instance.ExitGame();
    }

    public void OnNoButtonClicked()
    {
       GameManager.Instance.CancelExitGame();
    }
}
