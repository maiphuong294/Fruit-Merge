using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    static public UIManager instance { get; private set; }
    public HomeScreen homeScreen;
    public PlayScreen playScreen;
    public StoreScreen storeScreen;

    public override void Awake()
    {
        base.Awake();
        instance = this;
    }
    public void Start()
    {
        playScreen.UpdateIsLoadFruitsFromJson();
        OpenHomeScreen();

    }
    public void OpenHomeScreen()
    {
        playScreen.Fade();
        storeScreen.Fade();
        homeScreen.Appear();
        AudioManager.instance.PlayMusic(AudioManager.instance.homeMusic);
        PlayerDataManager.instance.UpdateBestScoreData();
    }
    public void OpenPlayScreen()
    {
        homeScreen.Fade();
        storeScreen.Fade();
        playScreen.Appear();
        AudioManager.instance.PlayMusic(AudioManager.instance.playMusic);
        Messenger.FireEvent(EventKey.OnPlayGame);
    }
    public void OpenStoreScreen()
    {
        homeScreen.Fade();
        playScreen.Fade();
        storeScreen.Appear();
        AudioManager.instance.PlayMusic(AudioManager.instance.shopMusic);
    }

    public void PlayScreenUpdateCurrentFruits()
    {
        print("goi tu uimanager");
        playScreen.UpdateCurrentFruits();
    }


}
