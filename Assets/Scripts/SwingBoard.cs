using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingBoard : MonoBehaviour
{
    void Start()
    {
        Swing();
    }

    public void Swing()
    {
        float duration = 1f;
        float angle = 3f;
        transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, -angle));
        transform.DORotate(new Vector3(0f, 0f, angle), duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
}
