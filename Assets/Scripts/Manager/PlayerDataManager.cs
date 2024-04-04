using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{  
    public static PlayerDataManager instance { get; private set; }
    public string saveFilePath;
    public PlayerData playerData;

    void Awake()
    {
        instance = this;
        saveFilePath = Application.persistentDataPath + "/PlayerData.json";
        
    }
    public void Start()
    {
        Messenger.AddListener(EventKey.OnPlayGame, UpdateBestScoreData);
        LoadData();
        Messenger.FireEvent(EventKey.OnGoldChange);
        Messenger.FireEvent(EventKey.OnBestScoreChange);
        Messenger.FireEvent(EventKey.OnSkinOwnedChange);
        Messenger.FireEvent(EventKey.OnCurrentSkinChange, playerData.currentSkinIndex);
        Messenger.FireEvent(EventKey.OnBackgroundOwnedChange);
        Messenger.FireEvent(EventKey.OnCurrentBackgroundChange, playerData.currentBackgroundIndex);
        for (int i = 0; i < 4; i++)
        {
            Messenger.FireEvent(EventKey.OnNumOfSuppliesChange, i);
        }
    }


    #region Update Player Data

    public void InitPlayerData()
    {
        playerData = new PlayerData();
        playerData.gold = 5000;
        playerData.bestScore = 0;
        for(int i = 0; i < playerData.numOfSupplies.Length; i++)
        {
            playerData.numOfSupplies[i] = 1;
        }
        for(int i = 0; i < playerData.settings.Length; i++)
        {
            playerData.settings[i] = true;
        }
        //skin
        playerData.currentSkinIndex = 0;
        playerData.skinOwned[0] = true;
        for(int i = 1; i < playerData.skinOwned.Length; i++)
        {
            playerData.skinOwned[i] = false;
        }
        //background
        playerData.currentBackgroundIndex = 0;
        playerData.backgroundOwned[0] = true;
        for (int i = 1; i < playerData.backgroundOwned.Length; i++)
        {
            playerData.backgroundOwned[i] = false;
        }
        //dailyreward
        playerData.lastClaimDay = "";
        playerData.currentDayReward = 0;

        print("init new player data");
    }

    public void UpdateGoldData(int amount)
    {
        playerData.gold += amount;
        print("update gold data");
        Messenger.FireEvent(EventKey.OnGoldChange);
    }
    public void UpdateBestScoreData()
    {
        int currentScore = ScoreManager.instance.currentScore;
        playerData.bestScore = (currentScore > playerData.bestScore) ? currentScore : playerData.bestScore;
        Messenger.FireEvent(EventKey.OnBestScoreChange);
    }

    public void UpdateSkinOwned(int index)
    {
        playerData.skinOwned[index] = true;
        Messenger.FireEvent(EventKey.OnSkinOwnedChange);
    }
    public void UpdateCurrentSkin(int index)
    {
        if (playerData.skinOwned[index]) { 
            playerData.currentSkinIndex = index;
            Messenger.FireEvent(EventKey.OnCurrentSkinChange, playerData.currentSkinIndex);
        }
        
    }

    public void UpdateBackgroundOwned(int index)
    {
        playerData.backgroundOwned[index] = true;
        Messenger.FireEvent(EventKey.OnBackgroundOwnedChange);
    }

    public void UpdateCurrentBackground(int index)
    {
        if (playerData.backgroundOwned[index])
        {
            playerData.currentBackgroundIndex = index;
            print("update current background in playdatamanger" + index);
            Messenger.FireEvent(EventKey.OnCurrentBackgroundChange, index);
        }
    }
    public void UpdateNumOfSuppliesData(int index, bool isAdded)
    {
        if (isAdded)
            playerData.numOfSupplies[index] += 1;
        else playerData.numOfSupplies[index] -= 1;
        Messenger.FireEvent(EventKey.OnNumOfSuppliesChange, index);
    }

    #endregion


    #region Save Load JSON
    public void LoadData()
    {
        if (File.Exists(saveFilePath))
        {
            
            string loadPlayerData = File.ReadAllText(saveFilePath);
            playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData);

            //update things
            print("load data");
            return;
        }
        InitPlayerData();
 
    }
    public void SaveData()
    {
        string savePlayerData = JsonUtility.ToJson(playerData);
        File.WriteAllText(saveFilePath, savePlayerData);
        Debug.Log("Save file created at: " + saveFilePath);
    }
    #endregion

    public void OnApplicationPause(bool isPaused)
    {
        if(isPaused) SaveData();
        Debug.Log("application pause");
    }
}