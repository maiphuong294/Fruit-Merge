using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiggestBallPopup : Popup
{
    public GameObject confirmButton;
    public CanvasGroup confirmButtonCanvasGroup;
    public void Awake()
    {
        confirmButtonCanvasGroup = confirmButton.GetComponent<CanvasGroup>();
    }
    public override void Open()
    {
        if (PlayerDataManager.instance.playerData.numOfSupplies[3] < 1)
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
        DestroyBiggestBall();
        PlayerDataManager.instance.UpdateNumOfSuppliesData(3, false);

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
        ParticleSystem effect = ObjectPool.instance.GetFromParticleSystemPool();
        effect.transform.position = list[maxID].transform.position;
        int sizee = list[maxID].GetComponent<Fruit>().getSize();
        effect.transform.localScale = Vector3.one * 2f * DataStorage.instance.sizes[sizee];
        effect.Play();
        AudioManager.instance.PlaySound(AudioManager.instance.mergeObject);
    }
}
