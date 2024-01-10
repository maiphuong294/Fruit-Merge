using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public GameObject greyBackground;
    public void Start()
    {
        gameObject.transform.localScale = Vector3.zero;       
    }

    public void Open()
    {
        greyBackground.SetActive(true);
        gameObject.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InBounce);
        
    }
    public void Close()
    {
        greyBackground.SetActive(false);
        gameObject.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.OutBounce); 
    }
}
