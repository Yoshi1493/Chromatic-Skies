using System.Collections.Generic;
using UnityEngine;

public abstract class GenericObjectPool<T> : MonoBehaviour
{
    public static GenericObjectPool<T> Instance { get; private set; }

    protected readonly List<(T projectile, Queue<T> queue)> objectPool = new();
    [HideInInspector] public new Transform transform;

    void Awake()
    {
        Instance = this;
        transform = GetComponent<Transform>();
    }

    public abstract void UpdatePoolableObjects(List<T> projectiles);

    public abstract T Get(int ID);

    public abstract void ReturnToPool(T returningObject);

    public void DrainPool()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        objectPool.Clear();
    }
}