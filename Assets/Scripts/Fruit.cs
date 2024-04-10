using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Fruit : MonoBehaviour
{
    [SerializeField] private int size;
    [SerializeField] private bool isMerged;

    private Rigidbody2D rigidBody;

    public bool CanDrop { get; private set; }

    private SpriteRenderer spriteRenderer;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        isMerged = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fruit"))
        {
            Fruit fruit2 = collision.gameObject.GetComponent<Fruit>();
            if (fruit2.getSize() == getSize() && isMerged == false)
            {
                updateIsMerged();
                fruit2.updateIsMerged();
                MergeObject(gameObject, collision.gameObject, getSize());
            }
        }
    }

    public void MergeObject(GameObject a, GameObject b, int size)
    {
        Vector3 middlePoint = Vector3.Lerp(a.transform.position, b.transform.position, 0.5f);
        a.SetActive(false);
        b.SetActive(false);
        if (DataStorage.instance.currentObjects[size + 1] != null)
        {
            AudioManager.instance.PlaySound(AudioManager.instance.mergeObject);
            SpawnObject(middlePoint, size + 1);
            ScoreManager.instance.AddScore(size * 3);
            ScoreManager.instance.comboCounter += 1;
            if (ScoreManager.instance.comboCounter >= 1)
            {
                Debug.Log("Combo " + ScoreManager.instance.comboCounter);
                Messenger.FireEvent(EventKey.OnShowCombo, ScoreManager.instance.comboCounter - 1);
            }
            Messenger.FireEvent(EventKey.OnUpCoinSlider);
        }

            

    }

    //up 1 size
    public void UpgradeObject()
    {
        Vector3 middlePoint = transform.position;  
        if (DataStorage.instance.currentObjects[size + 1] != null)
            SpawnObject(middlePoint, size + 1);
        gameObject.SetActive(false);
    }

    public void SpawnObject(Vector3 position, int size)
    {
        //GameObject a = Instantiate(DataStorage.instance.objectPrefab, position, Quaternion.identity);
        GameObject a = ObjectPool.instance.GetFromObjectPool();
        a.GetComponent<Fruit>().spawnSetup(size, position);
        ParticleSystem effect = ObjectPool.instance.GetFromParticleSystemPool();
        effect.transform.position = position;
        effect.transform.localScale = Vector3.one * 2f * DataStorage.instance.sizes[size];
        effect.Play();

    }

    public void spawnSetup(int size, Vector3 position)
    {
        isMerged = false;
        setPosition(position);
        setSize(size);
        setSkin(size);
        setScale(size);
        
        
        
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
    }

    public void setSkin(int size)
    {
        Sprite a = DataStorage.instance.currentObjects[size];
        spriteRenderer.sprite = a;
    }
    public void setScale(int size)
    {
        //size a la localscale
        float a = DataStorage.instance.sizes[size];
        setGlobalScale(new Vector3(a, a, a));

    }

    public void setGlobalScale(Vector3 globalScale)
    {
        //transform.localScale = Vector3.one;
        Vector3 target;
        if (transform.parent != null)
        {
            Vector3 parent = transform.parent.localScale;
            target = new Vector3(globalScale.x / parent.x, globalScale.y / parent.y, globalScale.z / parent.z);
        }
        else
        {
            //Debug.Log("Parent==null, use target global scale".Color("cyan"));
            target = globalScale;
        }

        //Debug.Log("global".Color("yellow") + globalScale + "target".Color("red") + target, gameObject);
        transform.DOKill();
        transform.localScale = Vector3.zero;
        transform.DOScale(target, 0.2f)
            .SetEase(Ease.OutCubic)
            .OnComplete(() => CanDrop = true);

        //Debug.Break();
    }

    public float getWidth()
    {
        return spriteRenderer.bounds.size.x;
    }

    public void Attach(Transform parent)
    {
        transform.SetParent(parent);
        rigidBody.isKinematic = true;
    }

    public void Drop()
    {
        transform.parent = null;
        rigidBody.isKinematic = false;
    }
}
