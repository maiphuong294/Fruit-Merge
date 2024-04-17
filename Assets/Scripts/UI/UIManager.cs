using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    static public UIManager instance { get; private set; }
    public HomeScreen homeScreen;
    public PlayScreen playScreen;
    public StoreScreen storeScreen;
    public IntroScreen introScreen;

    public bool isGamePlay;

    public override void Awake()
    {
        base.Awake();
        instance = this;
        
    }
    public void Start()
    {
        playScreen.UpdateIsLoadFruitsFromJson();
        OpenIntroScreen();
        
    }
    public void OpenHomeScreen()
    {
        playScreen.Fade();
        storeScreen.Fade();
        introScreen.Fade();
        homeScreen.Appear();
        AudioManager.instance.PlayMusic(AudioManager.instance.homeMusic);
        PlayerDataManager.instance.UpdateBestScoreData();
        SetIsGamePlay(false);
    }
    public void OpenPlayScreen()
    {
        homeScreen.Fade();
        storeScreen.Fade();
        introScreen.Fade();
        playScreen.Appear();
        AudioManager.instance.PlayMusic(AudioManager.instance.playMusic);
        Messenger.FireEvent(EventKey.OnPlayGame);
        SetIsGamePlay(true);
    }
    public void OpenStoreScreen()
    {
        homeScreen.Fade();
        playScreen.Fade();
        introScreen.Fade();
        storeScreen.Appear();
        AudioManager.instance.PlayMusic(AudioManager.instance.shopMusic);
    }

    public void OpenIntroScreen()
    {
        homeScreen.Fade();
        playScreen.Fade();
        storeScreen.Fade();
        introScreen.Appear();
        SetIsGamePlay(false);
    }

    public void PlayScreenUpdateCurrentFruits()
    {
        print("goi tu uimanager");
        playScreen.UpdateCurrentFruits();
    }

    public void SetIsGamePlay(bool a)
    {
        isGamePlay = a;
        Messenger.FireEvent(EventKey.OnGamePlay, isGamePlay);
    }


}
