using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    public RawImage theme;
    public Texture[] themes = new Texture[10];
    private float speedX = 0.05f, speedY = 0.05f;

    public void Awake()
    {
        Messenger.AddListener<int>(EventKey.OnCurrentBackgroundChange, ChangeTheme);
    }

    public void Update()
    {
        theme.uvRect = new Rect(theme.uvRect.position + new Vector2(speedX, speedY) * Time.deltaTime, theme.uvRect.size);
    }

    public void ChangeTheme(int id)
    {
        theme.texture = themes[id];
    }
}
