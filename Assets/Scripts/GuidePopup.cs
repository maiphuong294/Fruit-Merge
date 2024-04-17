using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class GuidePopup : MonoBehaviour
{
    public Image arrow, circleBar;
    public GameObject[] list = new GameObject[15];
    public GameObject fruitModel;

    [Range(0.3f, 1.5f)]
    public float[] listScale = new float[15];
    private int num = 11;
    public float radius;
    float maxFill = 0.95f;
    public float fillAmount, rotateAmount;
    public float[] angles = new float[15];
    float duration = 1.6f;

    
    [Button]
    void Awake()
    {
        radius = 285f;
        float mod = 0.9f;
        float delta = 0.02f;
        for (int i = 1; i <= num; i++)
        {
            float angle = 0.82f * (i - 1) * Mathf.PI * 2 / num * mod;
            mod += delta;
            angles[i] = angle;
   
            var pos = radius * new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f);
            list[i] = Instantiate(fruitModel, gameObject.transform).gameObject;
            list[i].transform.localPosition = pos;
            list[i].transform.DORotate(new Vector3(0f, 0f, -360f), 3f, RotateMode.FastBeyond360).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Incremental);
        }
    }

    [Button]
    public void Anim()
    {
        float delay = 0.1f;
        float timeEach = 0.13f;
        fillAmount = 0.92f;
        rotateAmount = 330f;
        circleBar.DOFillAmount(fillAmount, duration).SetEase(Ease.InOutSine);
        //circleBar.fillAmount = maxFill; 
        arrow.transform.DORotate(new Vector3(0f, 0f, -rotateAmount), duration, RotateMode.FastBeyond360).SetEase(Ease.InOutSine);
        
        for (int i = 1; i <= num; i++)
        {
            list[i].transform.DOScale(listScale[i] * Vector3.one, 0.2f).SetEase(Ease.OutBack).SetDelay(delay);
            delay += timeEach;
        }
    }

    [Button]
    public void ResetUI()
    {
        circleBar.fillAmount = 0f;
        arrow.transform.rotation = Quaternion.Euler(Vector3.zero);
        for(int i = 1; i <= num; i++)
        {
            list[i].transform.localScale = Vector3.zero;
        }
    }
    public void Open()
    {
        ResetUI();
        SetSkin();
        gameObject.SetActive(true);
        Anim();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void SetSkin()
    {
        print("set skin");
        for (int i = 1; i <= num; i++)
        {
            list[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = DataStorage.instance.currentSprites[i];
        }
    }

    [Button]
    public void SetScale()
    {
        for (int i = 1; i <= num; i++)
        {
            list[i].transform.localScale = listScale[i] * Vector3.one;
        }
    }
}
