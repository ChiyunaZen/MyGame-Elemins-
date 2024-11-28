using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    //ゲーム画面に移る
    public void StertNewGame()
    {
        SceneManager.LoadScene("Level1Scene");
    }

    
}
