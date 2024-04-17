using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LeaderBoardPopup : Popup
{
    public TextAsset textAssetData;
    public ListRanking listRanking;
    public List<KeyValuePair<string, int>> otherPlayerList;
    public TextMeshProUGUI score1, myscore, score2, name1, name2;


    public override void Start()
    {
        base.Start();
        listRanking.Setup();
        otherPlayerList = new List<KeyValuePair<string, int>>();
        ReadCSV();
        print(otherPlayerList);
    }
    public override void Open()
    {
        base.Open();
        SetValue();
        listRanking.ShowListRanking();
        print("open leader board popup");
    }


    public override void Close()
    {
        base.Close();
        listRanking.Setup();
    }

    public void SetValue()
    {
        int bestScore = PlayerDataManager.instance.playerData.bestScore;
        myscore.SetText(bestScore.ToString());

        int id = 0;
        for (int i = 0; i < otherPlayerList.Count; i++)
        {
            if (bestScore >= otherPlayerList[i].Value && bestScore < otherPlayerList[i + 1].Value)
            {
                id = i;
                break;        
            }
        }
        print("id " + id + "name above" + otherPlayerList[id + 1].Key.ToUpper());

        score1.SetText(otherPlayerList[id + 1].Value.ToString());
        name1.SetText(otherPlayerList[id + 1].Key.ToUpper());

        score2.SetText(otherPlayerList[id].Value.ToString());
        name2.SetText(otherPlayerList[id].Key.ToUpper());
    
    }

    public void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, System.StringSplitOptions.None);
        
        int numColumn = 2;
        int tableSize = data.Length / numColumn - 1;
 
        for (int i = 0; i < tableSize; i++)
        {
            otherPlayerList.Add(new KeyValuePair<string, int>(data[(i + 1)*numColumn], int.Parse(data[(i + 1)*numColumn + 1])));
        }
        otherPlayerList.Add(new KeyValuePair<string, int>("Helio", 0));
        otherPlayerList.Sort((x, y) => x.Value - y.Value);
       
    }

   
}
