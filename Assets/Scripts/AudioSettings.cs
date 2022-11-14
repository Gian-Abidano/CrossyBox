using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider sliderMaster;
    [SerializeField] Slider sliderBGM;
    [SerializeField] Slider sliderSFX;
    private float MasterValue;
    private float BGMValue;
    private float SFXValue;

    private void OnEnable() {
        LoadAudioSettings();
    }
    public void LoadAudioSettings()
    {
        SetMasterVolume(PlayerPrefs.GetFloat("Master", 1));
        sliderMaster.value = MasterValue;
        SetBGMVolume(PlayerPrefs.GetFloat("BGM", 1));
        sliderBGM.value = BGMValue;
        SetSFXVolume(PlayerPrefs.GetFloat("SFX", 1));
        sliderSFX.value = SFXValue;
    }

    public void SetMasterVolume(float sliderValue)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(sliderValue)*20);
        MasterValue = sliderValue;
    }
    
    public void SetBGMVolume(float sliderValue)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(sliderValue)*20);
        BGMValue = sliderValue;
    }
    
    public void SetSFXVolume(float sliderValue)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(sliderValue)*20);
        SFXValue = sliderValue;
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("Master", MasterValue);
        PlayerPrefs.SetFloat("BGM", BGMValue);
        PlayerPrefs.SetFloat("SFX", SFXValue);
        PlayerPrefs.Save();
    }
}
