using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPanel : MonoBehaviour
{
    public GameObject[] itemHolder = new GameObject[10];
    public GameObject[] greyCover = new GameObject[10];
    public GameObject[] priceHolder = new GameObject[10];
    public GameObject[] tick = new GameObject[10];
    public int numOfSkins = 5;
    public int[] prices = new int[10];

    public void Awake()
    {
        Messenger.AddListener(EventKey.OnSkinOwnedChange, OnGreyCover);
        Messenger.AddListener<int>(EventKey.OnCurrentSkinChange, OnTick);
    }
    public void Start()
    {   
        prices[0] = 1000;
        prices[1] = 2000;
        prices[2] = 2500;
        prices[3] = 3000;
        prices[4] = 5000;
    }
    public void OnPurchaseButton(int index)
    {
        if (PlayerDataManager.instance.playerData.skinOwned[index])
        {
            PlayerDataManager.instance.UpdateCurrentSkin(index);
            AudioManager.instance.PlayClickButtonSound();
            PickEffect(index);
            return;
            //use already owned skin
        }
        if (PlayerDataManager.instance.playerData.gold >= prices[index])
        {
            greyCover[index].SetActive(false);
            priceHolder[index].SetActive(false);
            PlayerDataManager.instance.UpdateSkinOwned(index);
            PlayerDataManager.instance.UpdateCurrentSkin(index);
            PlayerDataManager.instance.UpdateGoldData(-prices[index]);
            AudioManager.instance.PlaySound(AudioManager.instance.coin);
            Messenger.FireEvent(EventKey.OnPayCoinEffect);
            PickEffect(index);
        }
    }

    public void OnTick(int index)
    {
        print("on tick " + index);
        for (int i = 0; i < numOfSkins; i++)
        {
            if (i == index) tick[i].SetActive(true);
            else
            {
                tick[i].SetActive(false);
                itemHolder[i].transform.localScale = Vector3.one;
            }
        }
        print("on tick " + index);
    }

    public void OnGreyCover()
    {
        for (int i = 0; i < numOfSkins; i++)
        {
            if (PlayerDataManager.instance.playerData.skinOwned[i])
            {
                greyCover[i].SetActive(false);
                priceHolder[i].SetActive(false);
            }

            else
            {
                greyCover[i].SetActive(true);
                priceHolder[i].SetActive(true);
            }
        }
    }

    private void PickEffect(int id) {
        itemHolder[id].transform.DOScale(1.02f, 0.2f).SetEase(Ease.InOutSine);
    }



}
