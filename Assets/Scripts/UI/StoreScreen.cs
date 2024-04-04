using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreScreen : UIScreen
{
    public TextMeshProUGUI gold;
    private bool isInitialUpdateGold;
     private void Awake()
    {
        Messenger.AddListener(EventKey.OnGoldChange, UpdateGoldText);
    }

    public void UpdateGoldText()
    {
     
        gold.SetText(PlayerDataManager.instance.playerData.gold.ToString());
    }
}
