using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LeaderBoardPopup : Popup
{
    public ListRanking listRanking;

    public TextMeshProUGUI score1, myscore, score2;


    public override void Start()
    {
        base.Start();
        listRanking.Setup();
    }
    public override void Open()
    {
        base.Open();
        SetValue();
        listRanking.ShowListRanking();
    }


    public override void Close()
    {
        base.Close();
        listRanking.Setup();
    }

    public void SetValue()
    {
        int bestScore = PlayerDataManager.instance.playerData.bestScore;

        int score11 = bestScore + Random.Range(1, 300);
        score1.SetText(score11.ToString());

        myscore.SetText(bestScore.ToString());

        int score22 = Random.Range(Mathf.Max(0, bestScore - 300), bestScore);
        score2.SetText(score22.ToString());

    }

   
}
