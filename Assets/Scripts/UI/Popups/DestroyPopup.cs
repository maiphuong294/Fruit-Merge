using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DestroyPopup : Popup
{
    public void OnCancelButton()
    {
        base.Close();
    }
    public void OnConfirmButton()
    {
        base.Close();
        Destroy();
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
