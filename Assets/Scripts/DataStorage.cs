using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DataStorage : MonoBehaviour
{
    public static DataStorage instance { get; private set; }

    public GameObject objectPrefab;
    //public List<GameObject> fruits = new List<GameObject>();
    [Header ("Size")]
    [Range(0f, 0.5f)]
    public float[] sizes = new float[12];

    [Header ("Current Skin")]
    public Sprite[] currentObjects = new Sprite[12];


    [Header ("Skin Data")]
    public ArrayList skins = new ArrayList();

    public Sprite[] fruits = new Sprite[12];
    public Sprite[] balls = new Sprite[12];
    public Sprite[] planets = new Sprite[12];
    public Sprite[] candies = new Sprite[12];
    public Sprite[] meows = new Sprite[12];

    public void Awake()
    {
        instance = this;
        Debug.Log(Application.persistentDataPath);
        skins.Add(fruits);
        skins.Add(balls);
        skins.Add(planets);
        skins.Add(candies);
        skins.Add(meows);
        Messenger.AddListener<int>(EventKey.OnCurrentSkinChange, UpdateSkin);

    }

    public void UpdateSkin(int index)
    {
        Array.Copy((Array)skins[index], currentObjects, 12);
        print("update current objects " + index);
        UpdateCurrentObjectSkin();
    }
    public void UpdateCurrentObjectSkin()
    {
        List<GameObject> list = ObjectPool.instance.GetActiveObjects();
        foreach (GameObject obj in list)
        {
            var fruit = obj.GetComponent<Fruit>();
            fruit.setSkin(fruit.getSize());
        }
    }
}
