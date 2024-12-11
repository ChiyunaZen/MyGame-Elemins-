using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Sydewa;

public class UI_Loading : MonoBehaviour
{
    Animator animator;
    public Slider slider;
    GraphicRaycaster raycaster;
    //string targetScene; //遷移先のシーン名

    [SerializeField] LightingManager lightingManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        slider = GetComponentInChildren<Slider>();
        raycaster = GetComponent<GraphicRaycaster>();
    }

    private void Start()
    {
        ResetLoadingUI();
        Debug.Log(slider != null ? slider.gameObject.name : "スライダーが見つかりません");
    }




   public void ResetLoadingUI()
    {
        animator.SetBool("IsLoading", false);

        //ここで0.2秒待つ

        raycaster.enabled = false;
        slider.value = 0;

    }
    public void LoadingScene(string targetScene)
    {


        raycaster.enabled = true;
        StartCoroutine(LoadScene(targetScene));

    }

    IEnumerator LoadScene(string nextScene)
    {

        Debug.Log($"{nextScene}をローディング");
        animator.SetBool("IsLoading", true);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextScene); //読み込み状況を取得
                                                                                // asyncOperation.allowSceneActivation = false; //読み込み完了後自動で遷移しない


        // シーンロード進捗を20%がマックスになるように反映
        while (asyncOperation.progress < 0.9f)
        {
            slider.value = Mathf.Lerp(0f, 0.2f, asyncOperation.progress / 0.9f);
            yield return null;
        }

        

        slider.value = 0.2f; // 20%で固定

       

        //if (nextScene != "TitleScene") yield return new WaitForSeconds(1f);
      //  ResetLoadingUI();
    }
}
