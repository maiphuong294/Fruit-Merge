using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayScreen : UIScreen
{
    public TextMeshProUGUI currentScore;
    public void Start()
    {
        Messenger.AddListener(EventKey.OnCurrentScoreChange, UpdateCurrentScore);
    }

    public void Hide()
    {
        base.Hide();
    }
    public void Appear()
    {
        base.Appear();
    }
    public void DisAppear()
    {
        base.DisAppear();
    }
    public void UpdateCurrentScore()
    {
        currentScore.SetText(ScoreManager.instance.GetCurrentScore().ToString());
    }
}
