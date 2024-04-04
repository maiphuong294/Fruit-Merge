using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPrompt:MonoBehaviour
{
    private float scale = 1.05f;
    private float duration = 0.5f;
    private void Start()
    {
        PromptEffect(transform);
    }
    void PromptEffect(Transform transform)
    {
        transform.DOScale(scale, duration).SetEase(Ease.InSine).SetLoops(-1, LoopType.Yoyo);
    }
}
