using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmNewGamePopup : Popup
{
    public void OnCancelButton()
    {
        Close();
    }

    public void OnConfirmButton()
    {
        ToNewGame();
        Close();
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
