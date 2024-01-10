using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePopup : Popup
{
    public void OnCancelButton()
    {
        base.Close();
    }
    public void OnConfirmButton()
    {
        base.Close();
        UpGrade();
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
