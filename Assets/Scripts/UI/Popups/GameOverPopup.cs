using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverPopup : Popup
{
    [SerializeField] private TextMeshProUGUI currentScoreText;
    public void Awake()
    {
        Messenger.AddListener(EventKey.OnGameOver, UpdateCurrentScore);
        Messenger.AddListener(EventKey.OnGameOver, Open);
    }
    public override void Open()
    {
        base.Open();
        AudioManager.instance.PlaySound(AudioManager.instance.gameOver);
        AudioManager.instance.StopMusic();
    }
    public void OnReplayButton()
    {
        base.Close();
        Messenger.FireEvent(EventKey.OnPlayGame);
        AudioManager.instance.PlayMusic(AudioManager.instance.playMusic);
        DestroyAllBalls();  
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

    public void UpdateCurrentScore()
    {
        currentScoreText.SetText(ScoreManager.instance.currentScore.ToString());
    }
}
