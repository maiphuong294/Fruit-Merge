using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardPopup : Popup
{
    public Button[] buttons = new Button[10];
    private DateTime lastClaimDay;
    public override void Start()
    {
        base.Start();
        Setup();
        if (PlayerDataManager.instance.playerData.lastClaimDay == "")
        {
            lastClaimDay = DateTime.MinValue.Date;
        }
        else
        {
            lastClaimDay = DateTime.Parse(PlayerDataManager.instance.playerData.lastClaimDay).Date;
        }
        UpdateButtonsStatus();
        if (lastClaimDay != DateTime.Today.Date)
        {
            EnableClaimToday();
            PlayerDataManager.instance.playerData.currentDayReward += 1;
            PlayerDataManager.instance.playerData.lastClaimDay = DateTime.Today.Date.ToString();
        }
        
    }

    public void UpdateButtonsStatus()
    {
        int currentDay = PlayerDataManager.instance.playerData.currentDayReward;
        for (int i = 0; i < currentDay; i++)
        {
            UpdateTickedButton(i);
        } 
    }
    public void EnableClaimToday()
    {
        int currentDay = PlayerDataManager.instance.playerData.currentDayReward;
        buttons[currentDay].enabled = true;
    }
    public void OnClickButton(int id)
    {
        UpdateTickedButton(id);
        string amountStr = buttons[id].transform.Find("Amount Text").gameObject.GetComponent<TextMeshProUGUI>().text;
        int amount = int.Parse(amountStr);
        AudioManager.instance.PlaySound(AudioManager.instance.coin); 
        PlayerDataManager.instance.UpdateGoldData(amount);

    }

    public void UpdateTickedButton(int id)
    {
        Debug.Log("update tick button " + id);
        buttons[id].enabled = true;
        buttons[id].interactable = false;
        buttons[id].transform.Find("Tick Icon").gameObject.SetActive(true);
    }

    public void Setup()
    {
        for (int i = 0; i < 7; i++)
        {
            buttons[i].enabled = false;
            buttons[i].transform.Find("Tick Icon").gameObject.SetActive(false);
        }
    }
}
