using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public UIScreen homeScreen;
    public UIScreen playScreen;
    public void Start()
    {
        playScreen.Hide();
        homeScreen.Appear();
    }
    public void OpenHomeScreen()
    {
        playScreen.Hide();
        homeScreen.Appear();
    }
    public void OpenPlayScreen()
    {
        homeScreen.Hide();
        playScreen.Appear();
    }
}
