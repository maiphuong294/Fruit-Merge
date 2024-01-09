using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cloud : MonoBehaviour
{
    public static Cloud instance { get; private set; }
    [SerializeField] private GameObject holdPoint;
    private Fruit holdingFruit;
    private float distFromHoldPoint;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        holdingFruit = SpawnFruit(holdPoint.transform.position, 1);
        distFromHoldPoint = transform.position.x - holdPoint.transform.position.x;
    }

    public Vector3 getMousePos()
    {
        Vector3 mousePosScreen = Input.mousePosition;
        // Convert the mouse click position from screen space to world space
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
        return mousePosWorld;
    }

    public Fruit SpawnFruit(Vector3 position, int size)
    {
        GameObject fruitObject = ObjectPool.instance.GetFromPool();
        Fruit fruit = fruitObject.GetComponent<Fruit>();
        fruit.Attach(this.transform);
        fruit.spawnSetup(size, position);
        Debug.Log("Spawn object " + size);
        return fruit;
    }

    public async void DropObject()
    {
        if (holdingFruit == null)
            return;

        if (!holdingFruit.CanDrop)
            return;

        holdingFruit.Drop();
        holdingFruit = null;

        await Task.Delay(400);

        holdingFruit = SpawnFruit(holdPoint.transform.position, UnityEngine.Random.Range(1, 4));
    }
    public void followMouse()
    {
        Vector3 targetPos = new Vector3(getMousePos().x + distFromHoldPoint, transform.position.y, 0f);
        transform.position = targetPos;
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
