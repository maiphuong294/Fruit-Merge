using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class IntroScreen : UIScreen
{
    public GameObject[] list = new GameObject[15];
    public GameObject prefab;
    private int num = 11;
    private float radius = 1.2f;
    private float duration = 1.2f;
    public TextMeshProUGUI loading, tip;

    public override void Appear()
    {
        for (int i = 1; i <= num; i++)
        {
            list[i] = Instantiate(prefab, Vector3.zero, Quaternion.identity).gameObject;
            list[i].transform.SetParent(gameObject.transform);
            list[i].transform.localScale = Vector3.one;
            Sprite a = DataStorage.instance.fruits[i];
            list[i].transform.GetChild(2).gameObject.GetComponent<Image>().sprite = a;
            
        }
        base.Appear();
        Anim();
        LoadingText();
        TipText();
    }

    public async void Anim()
    {
        for (int i = 1; i <= num; i++)
        {
            float angle = i * Mathf.PI * 2 / num;
            var pos = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * radius;
            list[i].transform.eulerAngles = new Vector3(0f, 0f, - angle * 180f / Mathf.PI); 
            list[i].transform.DOMove(pos, duration).SetEase(Ease.InOutSine).SetLoops(6, LoopType.Yoyo);
            list[i].transform.DOScale(1.5f, duration).SetEase(Ease.InOutSine).SetLoops(6, LoopType.Yoyo);
            list[i].transform.DORotate(new Vector3(0f, 0f, - angle * 180f / Mathf.PI + 180f), duration, RotateMode.FastBeyond360).SetEase(Ease.InOutSine).SetLoops(6, LoopType.Incremental);
        }
        await Task.Delay(7200);
        UIManager.instance.OpenHomeScreen();

    }

    public async void LoadingText()
    {
        string[] messages = new string[5];
        messages[0] = "LOADING.";
        messages[1] = "LOADING..";
        messages[2] = "LOADING...";
        float timeEachUpdate = 0.2f;
        float numUpdate = duration * 6f / timeEachUpdate;
        for (int i = 0; i <= numUpdate; i++)
        {
            loading.text = messages[i % 3];
            await Task.Delay((int)(timeEachUpdate * 1000f)); 
        }
    }

    public async void TipText()
    {
        string[] messages = new string[5];
        messages[0] = "ITEMS WILL HELP YOU INCREASE YOUR SCORE MORE AND MORE.";
        messages[1] = "THE MORE COMBOS YOU MAKE. THE MORE COINS YOU WILL GET.";
        messages[2] = "PLAYING FOR MORE THAN 180 MINUTES A DAY WILL BADLY EFFECT YOUR HEALTH.";
        messages[3] = "REMEMBER TO CHECK-IN AND COLLECT YOUR DAILY GIFT.";
        float timeEachUpdate = 3f;
        float numUpdate = duration * 6f / timeEachUpdate;
        float timeFade = 0.5f;
        int ran = Random.Range(0, 4);
        tip.alpha = 1f;
        for (int i = 0; i <= numUpdate; i++)
        {
            tip.text = messages[(i + ran) % 4];
            tip.DOFade(1f, timeFade).SetEase(Ease.OutBack);
            tip.DOFade(0f, timeFade).SetEase(Ease.InBack).SetDelay(1.5f); ;
            await Task.Delay((int)(timeEachUpdate * 1000));
        }
    }
}

