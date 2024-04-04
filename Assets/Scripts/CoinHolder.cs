using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinHolder : MonoBehaviour
{
    public Vector3[] initialPos;
    public GameObject coinParent;
    public GameObject icon;
    private void Awake()
    {
        Messenger.AddListener(EventKey.OnPayCoinEffect, PayCoinEffect);
    }
    void Start()
    {
        initialPos = new Vector3[10];
        GetInitialPos();
        SetUp();
    }

    public void GetInitialPos()
    {
        for (int i = 0; i < coinParent.transform.childCount; i++)
        {
            initialPos[i] = coinParent.transform.GetChild(i).position;
        }
    }
    public void SetUp()
    {
        for (int i = 0; i < coinParent.transform.childCount; i++)
        {
            coinParent.transform.GetChild(i).position = icon.transform.position;
            Color a = coinParent.transform.GetChild(i).GetComponent<Image>().color;
            a.a = 1f;
            coinParent.transform.GetChild(i).GetComponent<Image>().color = a;
        }
    }
    public void PayCoinEffect()
    {
        float duration = 0.2f;
        float delay = 0f;
        for(int i = coinParent.transform.childCount - 1; i >= 0; i--)
        {
            coinParent.transform.GetChild(i).transform.DOMove(initialPos[i], duration).SetEase(Ease.InQuart).SetDelay(delay);
            coinParent.transform.GetChild(i).GetComponent<Image>().DOFade(0f, duration).SetEase(Ease.InQuart).SetDelay(delay + 0.1f);
            delay += 0.1f;
        }
        StartCoroutine("ResetCoins");
    }


    IEnumerator ResetCoins()
    {
        yield return new WaitForSeconds(0.2f);
        SetUp();
    }

}
