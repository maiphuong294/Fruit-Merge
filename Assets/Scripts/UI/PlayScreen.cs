using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayScreen : UIScreen
{
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI gold;
    public TextMeshProUGUI[] numOfSupplies = new TextMeshProUGUI[5];
    public bool isLoadFruitsFromJson;
    public void Awake()
    {
        Messenger.AddListener<int>(EventKey.OnNumOfSuppliesChange, UpdateNumOfSuppliesText);
        Messenger.AddListener(EventKey.OnGoldChange, UpdateGoldText);
        Messenger.AddListener(EventKey.OnCurrentScoreChange, UpdateCurrentScore);
    }

    public void Start()
    {   
        UpdateGoldText();
    }

    public void UpdateCurrentFruits()
    {
        if (isLoadFruitsFromJson)
        {
            ObjectPool.instance.UpdateCurrentFruits();
            isLoadFruitsFromJson = false;
        }
    }

    public void UpdateIsLoadFruitsFromJson()
    {
        isLoadFruitsFromJson = true;
    }


    public void UpdateCurrentScore()
    {
        currentScore.SetText(ScoreManager.instance.GetCurrentScore().ToString());
    }

    public void UpdateNumOfSuppliesText(int index)
    {
        int num = PlayerDataManager.instance.playerData.numOfSupplies[index];
        numOfSupplies[index].SetText(num.ToString());
    }

    public void UpdateGoldText()
    {
        gold.SetText(PlayerDataManager.instance.playerData.gold.ToString());
    }

}
