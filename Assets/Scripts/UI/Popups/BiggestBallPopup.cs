using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiggestBallPopup : Popup
{
    public void OnCancelButton()
    {
        base.Close();
    }
    public void OnConfirmButton()
    {
        base.Close();
        DestroyBiggestBall();
    }
    public void DestroyBiggestBall()
    {
        List<GameObject> list = ObjectPool.instance.GetActiveObjects();
        if (Cloud.instance.HoldingFruit != null)
        {
            list.Remove(Cloud.instance.HoldingFruit.gameObject);
        }
        int maxSize = 0;
        int maxID = -1;
        for (int i = 0; i < list.Count; i++)
        {
            int size = list[i].GetComponent<Fruit>().getSize();
            if (size > maxSize)
            {
                maxID = i;
                maxSize = size;
            }

        }
        if (maxID == -1) return;
        list[maxID].SetActive(false);
    }
}
