using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    public GameObject onButton, offButton;
    public Image image;
    public Sprite onBackground, offBackground;
    public void Start()
    {
        image = GetComponent<Image>();
        onButton = transform.GetChild(0).gameObject;
        offButton = transform.GetChild(1).gameObject;
        OffMusicButton();
    }
    public void OnMusicButton()
    {
        onButton.SetActive(false);
        offButton.SetActive(true);
        image.sprite = offBackground;
        AudioManager.instance.UpdateMusicVolume(0f);
    }

    public void OffMusicButton()
    {
        offButton.SetActive(false);
        onButton.SetActive(true);
        image.sprite = onBackground;
        AudioManager.instance.UpdateMusicVolume(0.7f);
    }
}
