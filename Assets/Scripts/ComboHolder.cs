using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComboHolder : MonoBehaviour
{
    public GameObject[] combo = new GameObject[10];
    public Vector3[] initialPos ;

    private void Awake()
    {
        Messenger.AddListener<int>(EventKey.OnShowCombo, ShowCombo);
    }
    void Start()
    {
        initialPos = new Vector3[10];
        GetInitialPos();
        SetUp();
    }

    public void ShowCombo(int id)
    {
        id -= 2;
        float timeFadeOut = 0.4f;
        combo[id].GetComponent<TextMeshProUGUI>().DOFade(1f, timeFadeOut).SetEase(Ease.OutQuad);
        combo[id].transform.DOScale(1f, timeFadeOut).SetEase(Ease.OutQuad);
        combo[id].transform.DOMove(initialPos[id], timeFadeOut).SetEase(Ease.OutQuad);
        combo[id].GetComponent<TextMeshProUGUI>().DOFade(0f, 0.4f).SetEase(Ease.OutQuad).SetDelay(1.1f).OnComplete(
            () => SetUpOneCombo(combo[id])
            ) ;
    }
    public void GetInitialPos()
    {
        for (int i = 0;  i < gameObject.transform.childCount; i++)
        {
            initialPos[i] = combo[i].transform.position;
        }
    }
    public void SetUp()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            combo[i].transform.position = transform.position;
            combo[i].transform.localScale = Vector3.zero;
            Color a = combo[i].GetComponent<TextMeshProUGUI>().color;
            a.a = 0f;
            combo[i].GetComponent<TextMeshProUGUI>().color = a;

        }
    }

    public void SetUpOneCombo(GameObject combo)
    {
        combo.transform.position = transform.position;
        combo.transform.localScale = Vector3.zero;
    }


}
