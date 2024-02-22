using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePopup : Popup
{
    public GameObject confirmButton;
    public CanvasGroup confirmButtonCanvasGroup;
    public void Awake()
    {
        confirmButtonCanvasGroup = confirmButton.GetComponent<CanvasGroup>();
    }
    public override void Open()
    {
        if (PlayerDataManager.instance.playerData.numOfSupplies[1] < 1)
        {
            confirmButtonCanvasGroup.interactable = false;
        }
        else
        {
            confirmButtonCanvasGroup.interactable = true;
        }
        base.Open();
    }
    public void OnCancelButton()
    {
        base.Close();
    }
    public void OnConfirmButton()
    {
        base.Close();
        UpGrade();
        PlayerDataManager.instance.UpdateNumOfSuppliesData(1, false);

    }

    public void UpGrade()
    {
        List<GameObject> list = ObjectPool.instance.GetActiveObjects();
        if (Cloud.instance.HoldingFruit != null)
        {
            list.Remove(Cloud.instance.HoldingFruit.gameObject);
        }
        HashSet<int> set = new HashSet<int>();
        while (set.Count < 2 && set.Count < list.Count)
        {
            set.Add(Random.Range(0, list.Count));
        }
        foreach (int i in set)
        {
            list[i].GetComponent<Fruit>().UpgradeObject();

        }
    }
}
