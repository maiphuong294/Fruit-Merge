using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    static public UIManager instance { get; private set; }
    public UIScreen homeScreen;
    public UIScreen playScreen;
    public UIScreen storeScreen;
    public override void Awake()
    {
        base.Awake();
        instance = this;
    }
    public void Start()
    {
        OpenHomeScreen();

    }
    public void OpenHomeScreen()
    {
        playScreen.Hide();
        storeScreen.Hide();
        homeScreen.Appear();
        AudioManager.instance.PlayMusic(AudioManager.instance.homeMusic);
    }
    public void OpenPlayScreen()
    {
        homeScreen.Hide();
        storeScreen.Hide();
        playScreen.Appear();
        AudioManager.instance.PlayMusic(AudioManager.instance.playMusic);
        Messenger.FireEvent(EventKey.OnPlayGame);
    }
    public void OpenStoreScreen()
    {
        homeScreen.Hide();
        playScreen.Hide();
        storeScreen.Appear();
        AudioManager.instance.PlayMusic(AudioManager.instance.shopMusic);
    }

}
