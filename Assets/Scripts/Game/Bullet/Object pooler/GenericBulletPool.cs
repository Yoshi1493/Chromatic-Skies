using System.Collections.Generic;
using UnityEngine;

public abstract class GenericBulletPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] T objectPrefab;
    Queue<T> objectQueue = new Queue<T>();

    public static GenericBulletPool<T> Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public T Get()
    {
        if (objectQueue.Count == 0) { AddObjects(1); }
        return objectQueue.Dequeue();
    }

    public void ReturnToPool(T returningObject)
    {
        returningObject.gameObject.SetActive(false);
        objectQueue.Enqueue(returningObject);
    }

    void AddObjects(int amount)
    {
        var newObject = Instantiate(objectPrefab, transform);
        newObject.gameObject.SetActive(false);

        objectQueue.Enqueue(newObject);
    }
}