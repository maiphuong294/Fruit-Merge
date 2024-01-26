using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorage : MonoBehaviour
{
    public static DataStorage instance { get; private set; }

    public GameObject objectPrefab;
    //public List<GameObject> fruits = new List<GameObject>();
    [Header ("Size")]
    [Range(0f, 0.5f)]
    public float[] sizes = new float[12];

    [Header ("Skin")]
    public Sprite[] fruits = new Sprite[12];



    public void Awake()
    {
        instance = this;
        Debug.Log(Application.persistentDataPath);
    }
}
