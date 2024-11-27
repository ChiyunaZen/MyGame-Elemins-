using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_IconChanger : MonoBehaviour
{
    [SerializeField] Image image; //変更するImageCompornent
    [SerializeField] Sprite defaulticon; //デフォルトアイコン
    [SerializeField] Sprite changedicon; //変更用のアイコン

    public void SetChangeIcon ()
    {
        if (changedicon != null)
        {
            image.sprite = changedicon;
        }
    }

    public void SetDefaultIcon ()
    {
        if (defaulticon != null)
        {
            image.sprite = defaulticon;
        }
    }

   
}
