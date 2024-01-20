using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLine : MonoBehaviour
{
    public float duration;
    public float fadeRate = 0.3f;
    public bool fadeDirection = false;
    public int numOfCollidingObjects;
    public bool IsChecking { get; private set; }
    [SerializeField] private Color color;
    private SpriteRenderer spriteRenderer;
    public void Start()
    {
        duration = 0f;
        IsChecking = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
        Messenger.AddListener(EventKey.OnPlayGame, ResetGameElements);   
    }
    public void Update()
    {
        if (!IsChecking) return;
        if (duration > 1f)
        {
            if (fadeDirection == false)
            {
                DownAlpha();
            }
            else
            {
                UpAlpha();
            }
            Debug.Log(spriteRenderer.color.a);      
        }
        
        if(duration > 3f)
        {
            Messenger.FireEvent(EventKey.OnGameOver);
            Debug.Log("ON GAME OVER");
            IsChecking = false;
            return;
        }

        if (numOfCollidingObjects > 0)
        {
            duration += Time.deltaTime;
        }
        else
        {
            duration = 0f;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        numOfCollidingObjects++;
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        numOfCollidingObjects--;
    }

    public void UpAlpha()
    {
        //color.a = Math.Min(1f, color.a + Time.deltaTime * fadeRate);
        color.a = 1f;
        if (color.a == 1f) fadeDirection = false;
        spriteRenderer.color = color;
    }
    public void DownAlpha()
    {
        color.a = Math.Max(0f, color.a - Time.deltaTime * fadeRate);
        if (color.a == 0f) fadeDirection = true;
        spriteRenderer.color = color;
    }

    public void ResetGameElements()
    {
        IsChecking = true;
        fadeDirection = false;

        Color c = spriteRenderer.color;
        c.a = 1f;
        spriteRenderer.color = c;
    }

}
