using UnityEngine;

public abstract class GenericObjectPool : MonoBehaviour
{
    public static GenericObjectPool Instance { get; private set; }

    [HideInInspector] public new Transform transform;

    void Awake()
    {
        Instance = this;
        transform = GetComponent<Transform>();
    }

    void Start()
    {
        UpdatePoolableObjects();
    }

    public abstract void UpdatePoolableObjects();

    public abstract GameObject Get(int ID);

    public abstract void ReturnToPool(GameObject returningObject, int ID);

    protected virtual void Disable(GameObject go)
    {
        go.SetActive(false);
    }

    public virtual void DrainPool()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}