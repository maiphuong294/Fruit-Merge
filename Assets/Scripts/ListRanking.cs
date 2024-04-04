using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListRanking : MonoBehaviour
{
    public Vector3[] initialPos;
    void Awake()
    {
        initialPos = new Vector3[10];
        GetInitialPos();
    }

    public void ShowListRanking()
    {
        float duration = 0.2f;
        float delay = 0.3f;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.DOLocalMove(initialPos[i], duration).SetEase(Ease.InOutBack).SetDelay(delay);
            transform.GetChild(i).transform.DOScale(1f, duration).SetEase(Ease.InOutBack).SetDelay(delay);
            delay += 0.2f;
        }
        Debug.Log("show list ranking");
    }

    public void Setup()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.localPosition = initialPos[i] + Vector3.down * 10f;
            transform.GetChild(i).transform.localScale = Vector3.zero;
        }
    }

    public void GetInitialPos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            initialPos[i] = transform.GetChild(i).transform.localPosition;
        }

    }
}
