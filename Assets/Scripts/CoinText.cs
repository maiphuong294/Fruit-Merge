using DG.Tweening.Core;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private void Awake()
    {
        Messenger.AddListener(EventKey.OnGoldChange, StartCoinsChange);
    }
    void Start()
    {
        coinText = GetComponent<TextMeshProUGUI>();
    }
    
    public void StartCoinsChange()
    {
        if (coinText.IsActive() == false) return;
        StartCoroutine("CoinsChange");
    }
    public IEnumerator CoinsChange()
    {
        
        float expectedDuration = 0.3f;
        int currentCoins = int.Parse(coinText.text);
        int expectedCoins = PlayerDataManager.instance.playerData.gold;
        int timesUpdate = 30;
        float timePerUpdate = expectedDuration / timesUpdate;
        int coinChangeEachTime = (currentCoins - expectedCoins) / 20;

        for (int i = 1; i < timesUpdate; i++)
        {
            currentCoins += coinChangeEachTime;
            coinText.SetText(currentCoins.ToString());
            yield return new WaitForSeconds(timePerUpdate);
        }
        coinText.SetText(expectedCoins.ToString());
        yield break;
      
    }
}
