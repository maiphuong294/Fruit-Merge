using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreScreen : UIScreen
{
    public TextMeshProUGUI gold;
    private bool isInitialUpdateGold;
    public TextMeshProUGUI[] txt;
    public Color picked, notPicked;
     private void Awake()
    {
        Messenger.AddListener(EventKey.OnGoldChange, UpdateGoldText);
    }
    public void Start()
    {
        OnPick(0);
    }

    public void UpdateGoldText()
    {
        gold.SetText(PlayerDataManager.instance.playerData.gold.ToString());
    }

    public void OnPick(int id)
    {
        for(int i = 0; i < 3; i++)
        {
            if (id == i)
            {
                txt[i].fontSize = 45;
                txt[i].color = picked;
            }
            else
            {
                txt[i].fontSize = 36;
                txt[i].color = notPicked;   
            }
        }
    }
}
