using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    Slider slider;
    [SerializeField] Slider masterSlider;
    [SerializeField] private bool isMasterSlider = false;　//自身がマスタースライダーかどうかインスペクターで設定
    AudioSource audioSource; // SE再生用
    public AudioClip sliderSEclip;
    private bool valueChanged = false; // スライダーの値が変更されたかを記録

    [SerializeField] private AudioManager audioManager;
    
    UI_IconChanger iconChanger;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        audioSource = GetComponent<AudioSource>();
        iconChanger = GetComponentInChildren<UI_IconChanger>();

        slider.onValueChanged.AddListener(value =>
        {
            valueChanged = true; // 値が変更されたらフラグを立てる

            UpdateIcon(value);  // アイコン更新処理を追加
            StartCoroutine(ResetValueChanged());
        });

        if (!isMasterSlider && audioManager != null)
        {
            // マスター音量変更時の影響を受ける
            audioManager.OnMasterVolumeChanged += CheckMasterVolume;
        }
    }

    private void Update()
    {
        // マウスボタンが離されたタイミングでSEを再生
        if (Input.GetMouseButtonUp(0) && valueChanged)
        {
            PlaySE(sliderSEclip);
            valueChanged = false; // フラグをリセット
        }
    }



    // SEを再生
    private void PlaySE(AudioClip clip)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(clip); // 指定されたSEを再生
        }
    }

    // スライダーの値に応じてアイコンを変更
    private void UpdateIcon(float value)
    {
        if (iconChanger == null) return;

        // 自身のスライダー値が0、またはマスタースライダーが0の場合
        if (Mathf.Approximately(value, 0f) || (masterSlider != null && Mathf.Approximately(masterSlider.value, 0f)))
        {
            iconChanger.SetChangeIcon();
        }
        else
        {
            iconChanger.SetDefaultIcon();
        }
    }

    //一定時間音量が変更されなかった場合にフラグをリセットする
    IEnumerator ResetValueChanged()
    {
        yield return new WaitForSeconds(0.5f);
        valueChanged = false; // フラグをリセット
    }

    private void CheckMasterVolume(float masterValue)
    {
        if (iconChanger == null) return;

        if (Mathf.Approximately(masterValue, 0f))
        {
            iconChanger.SetChangeIcon();
        }
        else if (!Mathf.Approximately(slider.value, 0f))
        {
            iconChanger.SetDefaultIcon();
        }
    }

   
}
