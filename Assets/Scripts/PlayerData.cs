using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance { get; set; }
    public void Awake()
    {
        instance = this;  
    }

    public void Start()
    {
        gold = 0;
        for (int i = 1; i <= 4; i++)
        {
            numOfSupplies[i] = 1;
        }
        for (int i = 1; i <= 3; i++)
        {
            settings[i] = true;
        }
        Messenger.FireEvent(EventKey.OnGoldChange);
        Messenger.AddListener(EventKey.OnGameOver, EarnGold);
    }

    public int gold;

    public int[] numOfSupplies = new int[5];
    public bool[] settings = new bool[5];
    public void EarnGold()
    {
        gold += ScoreManager.instance.GetCurrentScore() / 10;
        Messenger.FireEvent(EventKey.OnGoldChange);
    }

}




