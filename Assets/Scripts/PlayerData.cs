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
    

}




