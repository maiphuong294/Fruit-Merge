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
        Messenger.AddListener(EventKey.OnGameOver, UpdateGoldData);
        Messenger.AddListener(EventKey.OnPlayGame, UpdateBestScoreData);
        LoadData();
        Messenger.FireEvent(EventKey.OnGoldChange);
        Messenger.FireEvent(EventKey.OnBestScoreChange);
    }


    #region Update Player Data

    public void InitPlayerData()
    {
        playerData = new PlayerData();
        playerData.gold = 0;
        playerData.bestScore = 0;
        for(int i = 0; i < playerData.numOfSupplies.Length; i++)
        {
            playerData.numOfSupplies[i] = 0;
        }
        for(int i = 0; i < playerData.settings.Length; i++)
        {
            playerData.settings[i] = true;
        }
        print("init new player data");
    }

    public void UpdateGoldData()
    {
        playerData.gold += ScoreManager.instance.currentScore / 10;
        print("update gold data");
        Messenger.FireEvent(EventKey.OnGoldChange);
    }
    public void UpdateBestScoreData()
    {
        int currentScore = ScoreManager.instance.currentScore;
        playerData.bestScore = (currentScore > playerData.bestScore) ? currentScore : playerData.bestScore;
        Messenger.FireEvent(EventKey.OnBestScoreChange);
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