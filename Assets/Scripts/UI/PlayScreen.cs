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
    public void Awake()
    {
        Messenger.AddListener(EventKey.OnGoldChange, UpdateGoldText);
        Messenger.AddListener<int>(EventKey.OnNumOfSuppliesChange, UpdateNumOfSuppliesText);
    }

    public void Start()
    {
        Messenger.AddListener(EventKey.OnCurrentScoreChange, UpdateCurrentScore);
    }

    public void UpdateCurrentScore()
    {
        currentScore.SetText(ScoreManager.instance.GetCurrentScore().ToString());
    }
    public void UpdateGoldText()
    {
        gold.SetText(PlayerDataManager.instance.playerData.gold.ToString());
    }

    public void UpdateNumOfSuppliesText(int index)
    {
        int num = PlayerDataManager.instance.playerData.numOfSupplies[index];
        numOfSupplies[index].SetText(num.ToString());
        print("update num of supplies text " + num);
        print(num);
    }

}
