using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayScreen : UIScreen
{
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI gold;
    public override void Awake()
    {
        base.Awake();
        Messenger.AddListener(EventKey.OnGoldChange, UpdateGoldText);
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
        print("update gold text");
    }

}
