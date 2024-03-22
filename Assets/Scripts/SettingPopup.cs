using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : Popup
{
    public Slider soundSlider;
    public Slider musicSlider;

    public void Awake()
    {
        soundSlider.onValueChanged.AddListener(UpdateSoundVolume);
        musicSlider.onValueChanged.AddListener(UpdateMusicVolume);
    }

    public void UpdateSoundVolume(float volume)
    {
        AudioManager.instance.UpdateSoundVolume(volume);
    }
    public void UpdateMusicVolume(float volume)
    {
        AudioManager.instance.UpdateMusicVolume(volume);
    }
}
