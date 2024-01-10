using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance {  get; private set; }
    [SerializeField] private GameObject prefab;
    [SerializeField] private List<GameObject> pool = new List<GameObject>();
    private int numOfElements = 10;
    private int currentNumOfElements;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {   
        for (int i = 1; i <= numOfElements; i++)
        {
            pool.Add(CreateObject());
        }
        currentNumOfElements = numOfElements;
    }
    public GameObject GetFromPool()
    {
        for (int i = 0; i < currentNumOfElements; i++)
        {
            if (!pool[i].activeSelf)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }
        GameObject a = CreateObject();      
        a.SetActive(true);
        pool.Add(a);
        currentNumOfElements++;
        return a;
    }

    public GameObject CreateObject()
    {
        GameObject a = Instantiate(prefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        a.SetActive(false);    
        return a;
    }

    public List<GameObject> GetActiveObjects()
    {
        List<GameObject> list = new List<GameObject>();
        foreach (var obj in pool)
        {
            if (obj.activeSelf)
            {
                list.Add(obj);
            }
        }
        return list;
    } 

    
}
