using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer; // AudioMixerの参照
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private Slider seVolumeSlider;

    // 保存用キー
    private const string MasterVolumeKey = "MasterVolume";
    private const string BGMVolumeKey = "BGMVolume";
    private const string SEVolumeKey = "SEVolume";

    // マスター音量が変更されたときのイベント
    public event Action<float> OnMasterVolumeChanged;


    void Start()
    {
        // 初期値のロードとスライダーの設定
        float masterVolume = PlayerPrefs.GetFloat(MasterVolumeKey, 0.5f); 
        float bgmVolume = PlayerPrefs.GetFloat(BGMVolumeKey, 0.5f);
        float seVolume = PlayerPrefs.GetFloat(SEVolumeKey, 0.7f);

        masterVolumeSlider.value = masterVolume;
        bgmVolumeSlider.value = bgmVolume;
        seVolumeSlider.value = seVolume;

        SetVolume("MasterVolume", masterVolume);
        SetVolume("BGMVolume", bgmVolume);
        SetVolume("SEVolume", seVolume);

        // スライダー変更時のリスナー設定
        masterVolumeSlider.onValueChanged.AddListener(value =>
        {
            SetAndSaveVolume(MasterVolumeKey, "MasterVolume", value);
            OnMasterVolumeChanged?.Invoke(value); // マスター変更時のイベントを通知
        });

        bgmVolumeSlider.onValueChanged.AddListener(value => SetAndSaveVolume(BGMVolumeKey, "BGMVolume", value));
        seVolumeSlider.onValueChanged.AddListener(value => SetAndSaveVolume(SEVolumeKey, "SEVolume", value));
    }


    // 音量設定と保存
    private void SetAndSaveVolume(string prefsKey, string parameterName, float value)
    {
        PlayerPrefs.SetFloat(prefsKey, value);
        PlayerPrefs.Save();
        SetVolume(parameterName, value);
    }

    // AudioMixerに音量を適用
    private void SetVolume(string parameterName, float value)
    {
        float volume;
        if (value <= 0f)
        {
            volume = -80f; // ミュート時は-80dBに設定
        }
        else
        {
            volume = Mathf.Log10(value) * 20f; // 0～1を-80～0dBに変換
        }
        audioMixer.SetFloat(parameterName, volume);
    }

    //音量設定をリセット
    public void ResetAudioSettings()
    {
        PlayerPrefs.DeleteKey(MasterVolumeKey);
        PlayerPrefs.DeleteKey(BGMVolumeKey);
        PlayerPrefs.DeleteKey(SEVolumeKey);
        PlayerPrefs.Save();
        Start(); // 再読み込み
    }
}