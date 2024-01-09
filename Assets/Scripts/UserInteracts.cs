using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        if (!IsOverUI())
        cloud.followMouse();
    }
    private void OnMouseUp()
    {
        if (!IsOverUI())
        {
            cloud.followMouse();
            cloud.DropObject();
        }
        
    }
    public static bool IsOverUI()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

}
