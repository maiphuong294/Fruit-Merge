using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject[] theme = new GameObject[10];

    public void Awake()
    {
        GetThemeObject();
        Messenger.AddListener<int>(EventKey.OnCurrentBackgroundChange, ChangeTheme);
    }

    public void GetThemeObject()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            theme[i] = transform.GetChild(i).gameObject;
        }
    }
    public void ChangeTheme(int id)
    {
        print("change theme  " + id);
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == id)
            {
                theme[i].SetActive(true);
            }else theme[i].SetActive(false);
        }
    }
}
