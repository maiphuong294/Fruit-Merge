using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveAtStart : MonoBehaviour
{
   
    void Start()
    {
        gameObject.SetActive(false);
    }

}
