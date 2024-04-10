using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFitter : MonoBehaviour
{
    public SpriteRenderer rink;

    void Start()
    {
        Camera.main.orthographicSize = rink.bounds.size.x * Screen.height / Screen.width * 0.5f;
    }
}
