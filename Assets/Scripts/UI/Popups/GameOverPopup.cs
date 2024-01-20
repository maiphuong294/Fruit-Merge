using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPopup : Popup
{
    public void Awake()
    {
        Messenger.AddListener(EventKey.OnGameOver, Open);
    }
    public void OnReplayButton()
    {    
        base.Close();
        //Messenger.FireEvent(EventKey.OnPlayGame); -> cai nay dung de cap nhat ischecking cho warningline
        DestroyAllBalls();  
        //reset everything    
    }
    public void OnAdsButton()
    {
        base.Close();
        //temporarily ignore
    }

    public void DestroyAllBalls()
    {
        List<GameObject> list = ObjectPool.instance.GetActiveObjects();
        if (Cloud.instance.HoldingFruit != null)
        {
            list.Remove(Cloud.instance.HoldingFruit.gameObject);
        }
        foreach (GameObject obj in list)
        {
            obj.SetActive(false);
        }
    }
}
