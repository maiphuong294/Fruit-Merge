using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : Popup
{
    public SoundToggle soundToggle;
    public MusicToggle musicToggle;
    public void Awake()
    {
        Messenger.AddListener(EventKey.OnSettingStatusChange, UpdateSettingStatus);
    }
    public void UpdateSettingStatus()
    {
        //update sound
        if (PlayerDataManager.instance.playerData.settings[0])
        {
            soundToggle.OffSoundButton();
        } else soundToggle.OnSoundButton();

        //update music
        if (PlayerDataManager.instance.playerData.settings[1])
        {
            musicToggle.OffMusicButton();
        }
        else musicToggle.OnMusicButton();

    }

}
