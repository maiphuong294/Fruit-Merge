using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinSlider : MonoBehaviour
{

    public static CoinSlider instance {  get; private set; }
    public GameObject coinParent;
    public GameObject coinPosTargetObject;
    public Slider slider;
    public Vector3 coinPosTarget;
    public int numOfCoins;
    public Vector3[] initialCoinPos;
    public void Awake()
    {
        instance = this;
        slider.onValueChanged.AddListener(EarnCoinBonus);
        Messenger.AddListener(EventKey.OnUpCoinSlider, UpSliderValue);
        Messenger.AddListener<float>(EventKey.OnUpdateCoinSliderValue, SetSliderValue);
    }

    void Start()
    {
        initialCoinPos = new Vector3[20];
        numOfCoins = coinParent.transform.childCount;
        coinPosTarget = coinPosTargetObject.transform.position;
        GetInitialCoinsPos();
        SetupCoins();
        
        coinParent.SetActive(false);
    }


    public void GetInitialCoinsPos()
    {
        for (int i = 0; i < numOfCoins; i++)
        {
            initialCoinPos[i] = coinParent.transform.GetChild(i).transform.position;
        }
    }
    public void SetupCoins()
    {
        for (int i = 0; i < numOfCoins; i++) { 
            coinParent.transform.GetChild(i).transform.localScale = Vector3.zero;
        }
    }
    public void ResetCoins()
    {
        for (int i = 0; i < numOfCoins; i++)
        {
            coinParent.transform.GetChild(i).transform.position = initialCoinPos[i];
            coinParent.transform.GetChild(i).transform.localScale = Vector3.zero;
        }
    }

    public void EarnCoinBonus(float volume)
    {
        if (volume != 1f) return;
        StartCoroutine("DelayOnGoldChange");
        ResetCoins();
        coinParent.SetActive(true);
        float delay = 0f;
        for (int i = 0; i < numOfCoins; i++)
        {
            Transform coin = coinParent.transform.GetChild(i).gameObject.transform;
            coin.DOScale(1f, 0.3f).SetEase(Ease.OutBack).SetDelay(delay);
            coin.DOMove(coinPosTarget, 0.2f).SetEase(Ease.InOutBack).SetDelay(delay + 0.4f);
            coin.DOScale(0f, 0.2f).SetEase(Ease.InOutBack).SetDelay(delay + 0.8f);
            
            delay += 0.1f;
        }
    }
    public IEnumerator DelayOnGoldChange()
    {
        float delay = 0.3f;
        yield return new WaitForSeconds(delay);
        PlayerDataManager.instance.UpdateGoldData(200);
        slider.value = 0f;
        yield break;
    }

    public void UpSliderValue()
    {
        slider.value += 0.05f;
    }

    public void ResetCoinSlider()
    {
        slider.value = 0f;
    }

    public void SetSliderValue(float value)
    {
        slider.value = value;
    }

}
