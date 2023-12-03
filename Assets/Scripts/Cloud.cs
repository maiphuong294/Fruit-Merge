using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cloud : MonoBehaviour
{
    private GameObject holdPoint;
    private GameObject holdingObject;
    private float distFromHoldPoint;
    void Start()
    {
        holdPoint = transform.Find("Hold Point").gameObject;
        distFromHoldPoint = transform.position.x - holdPoint.transform.position.x;
        holdingObject = spawnObject(holdPoint.transform.position, 1);
    }
    public static bool IsOverUI()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public Vector3 getMousePos()
    {
        Vector3 mousePosScreen = Input.mousePosition;
        // Convert the mouse click position from screen space to world space
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
        return mousePosWorld;
    }

    public GameObject spawnObject(Vector3 position, int size)
    {
        GameObject a = Instantiate(DataStorage.instance.objectPrefab, position, Quaternion.identity);
        a.transform.SetParent(gameObject.transform, true);
        a.GetComponent<Rigidbody2D>().isKinematic = true;
        a.GetComponent<Fruit>().spawnSetup(size);    
        Debug.Log("Spawn object " + size);
        return a;
    }

    public async void dropObject()
    {
        if (holdingObject == null) return;
        holdingObject.transform.SetParent(null);
        holdingObject.GetComponent<Rigidbody2D>().isKinematic = false;
        holdingObject = null;
        await Task.Delay(400);
        holdingObject = spawnObject(holdPoint.transform.position, UnityEngine.Random.Range(1, 4));
    }
    public void followMouse()
    {
        Vector3 targetPos = new Vector3(getMousePos().x + distFromHoldPoint, transform.position.y, 0f);
        transform.position = targetPos;
    }
}
