using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{

    public void Start()
    {
        float worldSpriteWidth = GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        // get the screen height & width in world space units
        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = (worldScreenHeight / Screen.height) * Screen.width;

        // initialize new scale to the current scale
        Vector3 newScale = transform.localScale;

        // divide screen width by sprite width, set to X axis scale
        newScale.x = worldScreenWidth / worldSpriteWidth;


        transform.localScale = newScale;


    }


}
