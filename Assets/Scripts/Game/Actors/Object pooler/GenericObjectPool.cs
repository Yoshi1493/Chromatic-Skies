using UnityEngine;

public abstract class GenericObjectPool<T> : MonoBehaviour
{
    public static GenericObjectPool<T> Instance { get; private set; }

    [HideInInspector] public new Transform transform;

    void Awake()
    {
        Instance = this;
        transform = GetComponent<Transform>();

        UpdatePoolableObjects();
    }

    public abstract void UpdatePoolableObjects();

    public abstract T Get(int ID);

    public abstract void ReturnToPool(T returningObject, int ID);

    protected abstract void Disable(T go);

    public virtual void DrainPool()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}