using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadJSON : MonoBehaviour
{
    //public PlayerData playerData;
    public static SaveLoadJSON instance { get; private set; }
    public string saveFilePath;

    void Awake()
    {
        saveFilePath = Application.persistentDataPath + "/PlayerData.json";
    }
    public void Start()
    {
        LoadData();
            
    }

    public void LoadData()
    {
        Debug.Log("co chay vao day khong");
        if (File.Exists(saveFilePath))
        {
            Debug.Log("tim thay file json roi");
            string loadPlayerData = File.ReadAllText(saveFilePath);
            PlayerData.instance = JsonUtility.FromJson<PlayerData>(loadPlayerData);

            //update things
            Debug.Log("load data from file");
        }
    }
    public void SaveData()
    {
        string savePlayerData = JsonUtility.ToJson(PlayerData.instance);
        File.WriteAllText(saveFilePath, savePlayerData);
        Debug.Log("Save file created at: " + saveFilePath);
    }

    public void OnApplicationQuit()
    {
        SaveData();
        Debug.Log("application quit");
    }
}