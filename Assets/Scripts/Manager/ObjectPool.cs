using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance {  get; private set; }
    [SerializeField] private GameObject prefab;
    [SerializeField] private List<GameObject> pool = new List<GameObject>();

    [SerializeField] private ParticleSystem psPrefab;
    [SerializeField] private List<ParticleSystem> psPool = new List<ParticleSystem>();
    private int numOfElements = 10;
    private int currentNumOfElements;

    private int psNumOfElements = 20;
    private int psCurrentNumOfElements;
    private void Awake()
    {
        instance = this;
        Messenger.AddListener<bool>(EventKey.OnGamePlay, SetIsGamePlay);
    }
    void Start()
    {   

        

        for (int i = 1; i <= psNumOfElements; i++)
        {
            psPool.Add(CreateParticleSystem());
        }
        psCurrentNumOfElements = psNumOfElements;
    }
    public GameObject GetFromObjectPool()
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

    public ParticleSystem CreateParticleSystem()
    {
        ParticleSystem a = Instantiate(psPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        a.Clear();
        return a;
    }
    public ParticleSystem GetFromParticleSystemPool()
    {
        for (int i = 0; i < psCurrentNumOfElements; i++)
        {
            if (!psPool[i].isPlaying)
            {
                psPool[i].Clear();
                return psPool[i];
            }
        }
        ParticleSystem a = CreateParticleSystem();
        psPool.Add(a);
        psCurrentNumOfElements++;
        return a;
    }

    public void UpdateCurrentFruits()
    {
        for (int i = 0; i <  PlayerDataManager.instance.playerData.numFruits; i++)
        {
            GameObject a = Instantiate(prefab, PlayerDataManager.instance.playerData.fruitPositions[i], Quaternion.identity);
            a.GetComponent<Fruit>().spawnSetup(PlayerDataManager.instance.playerData.fruitSizes[i], a.transform.position);
            pool.Add(a);
        }
        while(pool.Count < numOfElements) 
        {
            pool.Add(CreateObject());
        }
        currentNumOfElements = pool.Count;
    }

    public void SetIsGamePlay(bool a)
    {
        List<GameObject> list = new List<GameObject>();
        list.AddRange(pool);
        if (Cloud.instance.HoldingFruit != null)
        {
            list.Remove(Cloud.instance.HoldingFruit.gameObject);
            print("remove holding fruit");
        }
            
        foreach (var obj in list)
        {
            Fruit fruit = obj.GetComponent<Fruit>();
            if (a == true)
            {
                fruit.Resume();
            }
            else
            {
                fruit.Pause();
            }
            
        }
    }
}
