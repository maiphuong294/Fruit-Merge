using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TestObserver : MonoBehaviour
{
 
    private void Awake()
    {
        
        Messenger.AddListener(EventKey.OnOpenPanel, OpenPanel);
        gameObject.SetActive(false);
    }

    public void OpenPanel()
    {
        gameObject.SetActive(true);
    }

    public void Click()
    {
        Messenger.FireEvent(EventKey.OnOpenPanel);
    }
}
