using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        UpGrade();
        PlayerDataManager.instance.UpdateNumOfSuppliesData(1, false);

    }

    public async void UpGrade()
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
