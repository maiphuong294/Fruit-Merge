using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    public GameObject onButton, offButton;
    public Image image;
    public Sprite onBackground, offBackground;
    public void Awake()
    {
        image = GetComponent<Image>();    
        onButton = transform.GetChild(0).gameObject;
        offButton = transform.GetChild(1).gameObject;
        OffSoundButton();
    }
    public void OnSoundButton()
    {
        onButton.SetActive(false);
        offButton.SetActive(true);
        image.sprite = offBackground;
        AudioManager.instance.UpdateSoundVolume(0f);
    }

    public void OffSoundButton()
    {
        offButton.SetActive(false);
        onButton.SetActive(true);
        image.sprite = onBackground;
        AudioManager.instance.UpdateSoundVolume(0.7f);
    }
}
