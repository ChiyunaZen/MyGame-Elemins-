using Sydewa;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Loading : MonoBehaviour
{
    Animator animator;
    Slider slider;
    GraphicRaycaster raycaster;
    //string targetScene; //遷移先のシーン名

    public event Action<string> OnSceneLoaded;

    [SerializeField] LightingManager lightingManager;

    bool isSceneReady;

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
        isSceneReady = true;
    }




    void ResetLoadingUI()
    {
        animator.SetBool("IsLoading", false);
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

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextScene);

        asyncOperation.completed += operation => OnSceneLoadComplete(nextScene);

        while (!asyncOperation.isDone)
        {
            slider.value = asyncOperation.progress; // 読み込み進行状況をスライダーに反映

            //Debug.Log($"Loading Progress: {asyncOperation.progress}");


            if (asyncOperation.progress >= 0.95f) // 
            {
                // スライダーを最大値に設定
                slider.value = 1f;
                yield return new WaitForSeconds(0.2f); // アニメーション完了待機

            }

            yield return null;
        }
        if (lightingManager.SunDirectionalLight == null)
        {
            //LightingManagerのSunDirectionalLightがnullならシーンのDirectionalLightをセットする
            lightingManager.SunDirectionalLight = GameObject.FindWithTag("DirectionalLight").GetComponent<Light>();
        }
        ResetLoadingUI();
    }

    private void OnSceneLoadComplete(string nextScene)
    {
        Debug.Log($"Scene {nextScene} loaded successfully!");

        StartCoroutine(UpdatePlayerAfterSceneLoad(nextScene));
    }

    IEnumerator UpdatePlayerAfterSceneLoad(string nextScene)
    {
        while (!isSceneReady)
        {
            yield return null; // 初期化完了まで待機
        }

        OnSceneLoaded?.Invoke(nextScene);

    }
}
