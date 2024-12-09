using Sydewa;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Loading : MonoBehaviour
{
    Animator animator;
    Slider slider;
    GraphicRaycaster raycaster;

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
        asyncOperation.allowSceneActivation = false;

        // ロード進行状況の更新
        yield return UpdateLoadingProgress(asyncOperation);

        // シーンのロード完了処理
        OnSceneLoadComplete(nextScene);
    }

    IEnumerator UpdateLoadingProgress(AsyncOperation asyncOperation)
    {
        while (!asyncOperation.isDone)
        {
            slider.value = Mathf.Clamp01(asyncOperation.progress / 0.9f); // 進捗をスライダーに反映
            Debug.Log($"Loading Progress: {slider.value * 100}%");

            if (asyncOperation.progress >= 0.9f)
            {
                slider.value = 1f;
                yield return new WaitForSeconds(0.2f); // アニメーション完了待機
                asyncOperation.allowSceneActivation = true; // シーンを有効化
            }

            yield return null;
        }
    }

    private void OnSceneLoadComplete(string nextScene)
    {
        Debug.Log($"Scene {nextScene} loaded successfully!");
        StartCoroutine(InitializeSceneAfterLoad(nextScene));
        ResetLighting();
        ResetLoadingUI();
    }

    IEnumerator InitializeSceneAfterLoad(string nextScene)
    {
        while (!isSceneReady)
        {
            yield return null; // 初期化完了まで待機
            Debug.Log("非同期シーンロード完了後にWaitが入った");
        }
        OnSceneLoaded?.Invoke(nextScene);
    }

    void ResetLighting()
    {
        if (lightingManager.SunDirectionalLight == null)
        {
            // LightingManagerのSunDirectionalLightがnullならシーンのDirectionalLightをセットする
            Light sceneLight = GameObject.FindWithTag("DirectionalLight")?.GetComponent<Light>();
            if (sceneLight != null)
            {
                lightingManager.SunDirectionalLight = sceneLight;
            }
            else
            {
                Debug.LogWarning("DirectionalLight が見つかりませんでした");
            }
        }
    }
}
