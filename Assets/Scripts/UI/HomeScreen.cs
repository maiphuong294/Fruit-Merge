using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HomeScreen : UIScreen
{
    public TextMeshProUGUI bestScore;
    public ConfirmNewGamePopup confirmPopup;
    public void Awake()
    {
        Messenger.AddListener(EventKey.OnBestScoreChange, UpdateBestScore);
    }

    public void UpdateBestScore()
    {
        bestScore.SetText(PlayerDataManager.instance.playerData.bestScore.ToString());
    }

    public void OnNewGameButton()
    {
        print("on new game button ishavegame yet " + PlayerDataManager.instance.playerData.isHaveGameYet);
        if (PlayerDataManager.instance.playerData.isHaveGameYet == true)
        {
            confirmPopup.Open();
            return;
        }
        ToNewGame();

    }

    public void ToNewGame()
    {
        UIManager.instance.OpenPlayScreen();
        PlayerDataManager.instance.UpdateIsHaveGameYet();
        Messenger.FireEvent(EventKey.OnPlayGame);
        ScoreManager.instance.ResetScore();
        CoinSlider.instance.ResetCoinSlider();
        DestroyAllBalls();
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

}
