using System.Collections;
using System.Collections.Generic;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public UIScreen homeScreen;
    public UIScreen playScreen;
    public UIScreen storeScreen;
    public void Start()
    {
        playScreen.Hide();
        storeScreen.Hide();
        homeScreen.Appear();
   
    }
    public void OpenHomeScreen()
    {
        playScreen.Hide();
        storeScreen.Hide();
        homeScreen.Appear();
    }
    public void OpenPlayScreen()
    {
        homeScreen.Hide();
        storeScreen.Hide();
        playScreen.Appear();
        Messenger.FireEvent(EventKey.OnPlayGame);
    }
    public void OpenStoreScreen()
    {
        homeScreen.Hide();
        playScreen.Hide();
        storeScreen.Appear();
    }
}
