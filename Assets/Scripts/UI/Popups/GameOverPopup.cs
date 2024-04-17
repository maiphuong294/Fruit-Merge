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
        Messenger.AddListener(EventKey.OnGameOver, UpdateCurrentScoreText);
        Messenger.AddListener(EventKey.OnGameOver, Open);
        
    }
    public override void Open()
    {
        base.Open();
        AudioManager.instance.PlaySound(AudioManager.instance.gameOver);
        AudioManager.instance.StopMusic();
        UpdateCurrentBestScoreText();
        PlayerDataManager.instance.UpdateBestScoreData();
        UIManager.instance.SetIsGamePlay(false);
    }
    public void OnReplayButton()
    {
        UpdateCurrentBestScoreText();
        base.Close();
        Messenger.FireEvent(EventKey.OnPlayGame);
        ScoreManager.instance.ResetScore();
        CoinSlider.instance.ResetCoinSlider();
        AudioManager.instance.PlayMusic(AudioManager.instance.playMusic);
        DestroyAllBalls();
        UIManager.instance.SetIsGamePlay(true);
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

    public void UpdateCurrentScoreText()
    {
        currentScoreText.SetText(ScoreManager.instance.currentScore.ToString());
    }
    public void UpdateCurrentBestScoreText()
    {
        if (ScoreManager.instance.currentScore > PlayerDataManager.instance.playerData.bestScore)
        {
            bestScoreText.SetText("NEW BEST: " + ScoreManager.instance.currentScore.ToString());
        }
        else
        {
            bestScoreText.SetText("BEST: " + PlayerDataManager.instance.playerData.bestScore.ToString());
        }
        
    }
}
