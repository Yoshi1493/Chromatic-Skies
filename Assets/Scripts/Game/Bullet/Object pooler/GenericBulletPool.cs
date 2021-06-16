using System.Collections.Generic;
using UnityEngine;

public abstract class GenericBulletPool<T> : MonoBehaviour where T : Bullet
{
    public static GenericBulletPool<T> Instance { get; private set; }

    [SerializeField] protected ShipObject shipData;

    protected List<T> objectPrefabs = new List<T>();
    List<Queue<T>> objectQueue = new List<Queue<T>>();

    protected virtual void Awake()
    {
        Instance = this;

        for (int i = 0; i < shipData.bullets.Count; i++)
        {
            objectQueue.Add(new Queue<T>());
            objectPrefabs.Add(shipData.bullets[i].GetComponent<T>());
        }
    }

    public T Get(int index)
    {
        if (objectQueue[index].Count == 0) ExpandPool(index);
        return objectQueue[index].Dequeue();
    }

    public void ReturnToPool(int index, T returningObject)
    {
        returningObject.gameObject.SetActive(false);
        objectQueue[index].Enqueue(returningObject);
    }

    void ExpandPool(int index)
    {
        var newObject = Instantiate(objectPrefabs[index], transform);
        newObject.gameObject.SetActive(false);

        objectQueue[index].Enqueue(newObject);
    }
}