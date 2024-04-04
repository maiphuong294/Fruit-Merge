using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPanel : MonoBehaviour
{

    public GameObject[] purchaseButtons = new GameObject[5];
    public CanvasGroup[] purchaseButtonCanvasGroups = new CanvasGroup[5];
    public int[] prices = new int[5];
    public int defaultPrices = 500;
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            purchaseButtonCanvasGroups[i] = purchaseButtons[i].GetComponent<CanvasGroup>();
            prices[i] = defaultPrices;
        }
        Messenger.AddListener(EventKey.OnGoldChange, UpdatePurchaseButtons);
    }

    public void UpdatePurchaseButtons()
    {
        for (int i = 0; i < 4; i++)
        {
            if (PlayerDataManager.instance.playerData.gold >= prices[i])
            {
                purchaseButtonCanvasGroups[i].interactable = true;
            }else purchaseButtonCanvasGroups[i].interactable = false;
        }
    }

    public void OnPurchaseButton(int index)
    { 
        if (PlayerDataManager.instance.playerData.gold >= prices[index])
        {
            AudioManager.instance.PlaySound(AudioManager.instance.coin);
            PlayerDataManager.instance.UpdateGoldData(-prices[index]);
            PlayerDataManager.instance.UpdateNumOfSuppliesData(index, true);
            Messenger.FireEvent(EventKey.OnPayCoinEffect);
        }
        print("on purchase button");
    }
}
