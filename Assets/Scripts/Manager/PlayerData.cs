using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int gold;

    public int bestScore;

    public int[] numOfSupplies = new int[5];
    public bool[] settings = new bool[5];

    public int currentSkinIndex;
    public bool[] skinOwned = new bool[10];

    public int currentBackgroundIndex;
    public bool[] backgroundOwned = new bool[10];

    public string lastClaimDay;
    public int currentDayReward;

    public int numFruits;
    public List<int> fruitSizes = new List<int>();    
    public List<Vector3> fruitPositions = new List<Vector3>();

    public bool isHaveGameYet;

    public int currentScore;
    public float coinSliderValue;
    

}




