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
    public void OnReplayButton()
    {
        //Debug.Log("chay den day truoc");
        base.Close();
        //Debug.Log("chay den day");
        Messenger.FireEvent(EventKey.OnPlayGame);// -> cai nay dung de cap nhat ischecking cho warningline
        //Debug.Log("roi chay den day");
        DestroyAllBalls();  
        //reset everything    
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
