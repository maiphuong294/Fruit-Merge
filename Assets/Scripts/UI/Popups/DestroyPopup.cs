using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

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
        Destroy();
        PlayerDataManager.instance.UpdateNumOfSuppliesData(0, false);
    }
    public async void Destroy()
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
        foreach (int i in set)
        {
            list[i].SetActive(false);
            AudioManager.instance.PlaySound(AudioManager.instance.mergeObject);
            ParticleSystem effect = ObjectPool.instance.GetFromParticleSystemPool();
            effect.transform.position = list[i].transform.position;
            int size = list[i].GetComponent<Fruit>().getSize();
            effect.transform.localScale = Vector3.one * 2f * DataStorage.instance.sizes[size];
            effect.Play();
            await Task.Delay(100);
        }

    }

}
