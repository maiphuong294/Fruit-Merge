using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof (SpriteRenderer))]
public class Fruit : MonoBehaviour
{
    [SerializeField] private int size;
    [SerializeField] private bool isMerged;
    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log("get sprite renderer");
        isMerged = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fruit"))
        {
            Fruit fruit2 = collision.gameObject.GetComponent<Fruit>();
            if(fruit2.getSize() == getSize() && isMerged == false)
            {
                updateIsMerged();

                fruit2.updateIsMerged(); 
                MergeObject(gameObject, collision.gameObject, getSize());
            }
            
            
        }
    }

    public async void MergeObject(GameObject a, GameObject b, int size)
    {
 
        Vector3 middlePoint = Vector3.Lerp(a.transform.position, b.transform.position, 0.5f);
        //Destroy(a);
        //Destroy(b);
        a.SetActive(false);
        b.SetActive(false);
        await Task.Delay(500);
        if (DataStorage.instance.fruits[size + 1] != null)
            spawnObject(middlePoint, size + 1);
    }



    public void spawnObject(Vector3 position, int size)
    {
        //GameObject a = Instantiate(DataStorage.instance.objectPrefab, position, Quaternion.identity);
        GameObject a = ObjectPool.instance.GetFromPool();
        a.GetComponent<Fruit>().spawnSetup(size, position);
        Debug.Log("Spawn object " + size);
    }

    public void spawnSetup(int size, Vector3 position)
    {
        setPosition(position);
        setSize(size);
        setScale(size);
        setSkin(size);
        isMerged = false;
    }
    private void updateIsMerged()
    {
        isMerged = true;
    }

    public int getSize()
    {
        return size;
    }
    public void setPosition(Vector3 position)
    {
        transform.position = position;
    }
    public void setSize(int size)
    {
        this.size = size;
        Debug.Log("Set size " + size);
    }

    public void setSkin(int size)
    {
        Debug.Log("setSkin" + size);
        Sprite a = DataStorage.instance.fruits[size];
        spriteRenderer.sprite = a;
        //bug o day
    }
    public void setScale(int size)
    {
        float a = DataStorage.instance.sizes[size];
        setGlobalScale(transform, new Vector3(a, a, a));
        Debug.Log("set scale " + a);
    }



    public void setGlobalScale(Transform transform, Vector3 globalScale)
    {
        transform.localScale = Vector3.one;
        transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x, globalScale.y / transform.lossyScale.y, globalScale.z / transform.lossyScale.z);
    }


}
