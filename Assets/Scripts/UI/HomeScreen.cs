using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HomeScreen : UIScreen
{
    public TextMeshProUGUI bestScore;
    public override void Awake()
    {
        base.Awake();
        Messenger.AddListener(EventKey.OnBestScoreChange, UpdateBestScore);
    }

    public void UpdateBestScore()
    {
        bestScore.SetText(PlayerDataManager.instance.playerData.bestScore.ToString());
    }
}
