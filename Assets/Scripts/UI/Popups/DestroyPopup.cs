using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPopup : Popup
{
    public GameObject confirmButton;
    public CanvasGroup confirmButtonCanvasGroup;
    public void Awake()
    {
        confirmButtonCanvasGroup = confirmButton.GetComponent<CanvasGroup>();
    }
    public override void Open()
    {
        Debug.Log("open destroy popup");
        if (PlayerDataManager.instance.playerData.numOfSupplies[0] < 1)
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
        Destroy();
        PlayerDataManager.instance.UpdateNumOfSuppliesData(0, false);
    }
    public void Destroy()
    {
        List<GameObject> list = ObjectPool.instance.GetActiveObjects();
        if (Cloud.instance.HoldingFruit != null)
        {
            list.Remove(Cloud.instance.HoldingFruit.gameObject);
        }  
        HashSet<int> set = new HashSet<int>();
        while(set.Count < 4 && set.Count < list.Count)
        {
            set.Add(Random.Range(0, list.Count));
        }
        foreach(int i in set)
        {
            list[i].SetActive(false);
        }
    }

}
