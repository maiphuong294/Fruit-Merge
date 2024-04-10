using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverPopup : Popup
{
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    public void Awake()
    {
        Messenger.AddListener(EventKey.OnGameOver, UpdateCurrentScore);
        Messenger.AddListener(EventKey.OnGameOver, Open);
        UpdateCurrentBestScore();
    }
    public override void Open()
    {
        base.Open();
        AudioManager.instance.PlaySound(AudioManager.instance.gameOver);
        AudioManager.instance.StopMusic();
        PlayerDataManager.instance.UpdateBestScoreData();
    }
    public void OnReplayButton()
    {
        UpdateCurrentBestScore();
        base.Close();
        Messenger.FireEvent(EventKey.OnPlayGame);
        ScoreManager.instance.ResetScore();
        CoinSlider.instance.ResetCoinSlider();
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
    public void UpdateCurrentBestScore()
    {
        bestScoreText.SetText("BEST: " + PlayerDataManager.instance.playerData.bestScore.ToString());
    }
}
