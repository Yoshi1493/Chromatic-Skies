using System.Collections.Generic;
using UnityEngine;

public abstract class GenericBulletPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] List<T> objectPrefabs = new List<T>();
    List<Queue<T>> objectQueue = new List<Queue<T>>();

    public static GenericBulletPool<T> Instance { get; private set; }

    void Awake()
    {
        Instance = this;

        for (int i = 0; i < objectPrefabs.Count; i++)
        {
            objectQueue.Add(new Queue<T>());
        }
    }

    public T Get(int index)
    {
        if (objectQueue[index].Count == 0) AddObjects(index, 1);
        return objectQueue[index].Dequeue();
    }

    public void ReturnToPool(int index, T returningObject)
    {
        returningObject.gameObject.SetActive(false);
        objectQueue[index].Enqueue(returningObject);
    }

    void AddObjects(int index, int amount)
    {
        var newObject = Instantiate(objectPrefabs[index], transform);
        newObject.gameObject.SetActive(false);

        for (int i = 0; i < amount; i++)
        {
            objectQueue[index].Enqueue(newObject);
        }
    }
}