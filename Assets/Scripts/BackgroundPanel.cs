using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundPanel : MonoBehaviour
{
    public Button[] buttons = new Button[10];
    public GameObject[] blueBackground = new GameObject[10];
    public GameObject[] greyCover = new GameObject[10];
    public GameObject[] priceHolder = new GameObject[10];
    public GameObject[] tick = new GameObject[10];
    public int[] price = new int[10];

    public void Awake()
    {
        GetAllChildObjects();
        Messenger.AddListener(EventKey.OnBackgroundOwnedChange, Setup);
    }

    public void GetAllChildObjects()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            blueBackground[i] = buttons[i].transform.GetChild(0).gameObject;
            greyCover[i] = buttons[i].transform.GetChild(3).gameObject;
            priceHolder[i] = buttons[i].transform.GetChild(4).gameObject;
            tick[i] = buttons[i].transform.GetChild(5).gameObject;
            price[i] = int.Parse(priceHolder[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text);
        }
    }


    public void OnPurchaseButton(int id)
    {
        if (PlayerDataManager.instance.playerData.backgroundOwned[id] == true)
        {
            PlayerDataManager.instance.UpdateCurrentBackground(id);
            OnTickButton(id);
            AudioManager.instance.PlayClickButtonSound();
            return;
        }
        if (PlayerDataManager.instance.playerData.gold >= price[id])
        {
            PlayerDataManager.instance.UpdateGoldData(-price[id]);
            PlayerDataManager.instance.UpdateBackgroundOwned(id);
            PlayerDataManager.instance.UpdateCurrentBackground(id);
            AudioManager.instance.PlaySound(AudioManager.instance.coin);
            Messenger.FireEvent(EventKey.OnPayCoinEffect);
            PurchasedButton(id);
            OnTickButton(id);
        }
    }

    public void Setup()
    {
        for (int i = 0; i < buttons.Length; i++)
        {   
            if (PlayerDataManager.instance.playerData.backgroundOwned[i] == true)
            {
                PurchasedButton(i);
            }
            else
            {
                UnPurchasedButton(i);
            }
        }
        OnTickButton(PlayerDataManager.instance.playerData.currentBackgroundIndex);
    }
    public void UnPurchasedButton(int id)
    {
        greyCover[id].SetActive(true);
        priceHolder[id].SetActive(true);
    }

    public void PurchasedButton(int id)
    {
        Debug.Log("purchasedButton " + id);
        greyCover[id].SetActive(false);
        priceHolder[id].SetActive(false);
        
    }

    public void OnTickButton(int id)
    {
        tick[id].SetActive(true);
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == id)
            {
                blueBackground[i].SetActive(true);
                tick[i].SetActive(true);
            }
            else
            {
                blueBackground[i].SetActive(false);
                tick[i].SetActive(false);
            }
        }
        
    }
}
