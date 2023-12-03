using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInteracts : MonoBehaviour
{
    [SerializeField] private GameObject cloudObject;
    private Cloud cloud;
    private void Awake()
    {
        cloud = cloudObject.GetComponent<Cloud>();
    }

    private void OnMouseDrag()
    {
        cloud.followMouse();
    }
    private void OnMouseUp()
    {
        cloud.followMouse();
        cloud.dropObject();
    }
}
