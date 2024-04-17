using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ShufflePopup : Popup
{
    public GameObject confirmButton;
    public CanvasGroup confirmButtonCanvasGroup;
    public void Awake()
    {
        confirmButtonCanvasGroup = confirmButton.GetComponent<CanvasGroup>();
    }
    public override void Open()
    {
        if (PlayerDataManager.instance.playerData.numOfSupplies[2] < 1)
        {
            confirmButtonCanvasGroup.interactable = false;
        }
        else
        {
            confirmButtonCanvasGroup.interactable = true;
        }
        base.Open();
        UIManager.instance.SetIsGamePlay(false);
    }
    public void OnCancelButton()
    {
        base.Close();
        UIManager.instance.SetIsGamePlay(true);
    }
    public void OnConfirmButton()
    {
        base.Close();
        UIManager.instance.SetIsGamePlay(true);
        Shuffle();
        PlayerDataManager.instance.UpdateNumOfSuppliesData(2, false);

    }

    public async void Shuffle()
    {   
        //remove holding Object
        List<GameObject> list = ObjectPool.instance.GetActiveObjects();
        if (Cloud.instance.HoldingFruit != null)
        {
            list.Remove(Cloud.instance.HoldingFruit.gameObject);
        }

        //save all in lists before change
        List<Vector3> positions = new List<Vector3>();
        List<int> sizes = new List<int>();
        List<int> a = new List<int>();
        for (int i = 0; i < list.Count; i++)
        {
            a.Add(i);
            positions.Add(list[i].transform.position);
            sizes.Add(list[i].GetComponent<Fruit>().getSize());
        }

        //get order to shuffle
        List<int> b = new List<int>();
        while(a.Count > 0)
        {
            int tmp = Random.Range(0, a.Count);
            b.Add(a[tmp]);
            a.RemoveAt(tmp);       
        }

        //shuffle
        for (int i = 0; i < list.Count; i++)
        {
            list[i].SetActive(false);         
        }
        for (int i = 0; i < list.Count; i++)
        {
            await Task.Delay(30);
            SpawnObject(positions[b[i]], sizes[i]);
        }

    }
    public void SpawnObject(Vector3 position, int size)
    {
        GameObject a = ObjectPool.instance.GetFromObjectPool();
        a.GetComponent<Fruit>().spawnSetup(size, position);
        AudioManager.instance.PlaySound(AudioManager.instance.mergeObject);
    }
}
