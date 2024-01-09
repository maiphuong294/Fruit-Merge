using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIScreen : MonoBehaviour
{
   
    public CanvasGroup canvasGroup;
    public void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    
    public void Hide()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        gameObject.SetActive(false);
    }
    public void Appear()
    {
        gameObject.SetActive(true);
        canvasGroup.interactable = true;
        canvasGroup.DOFade(1f, 0.2f);
    }
    public void DisAppear()
    {
        gameObject.SetActive(false);
        canvasGroup.DOFade(0f, 0.2f);
    }

}
