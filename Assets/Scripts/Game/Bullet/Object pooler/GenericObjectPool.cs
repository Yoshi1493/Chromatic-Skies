using System.Collections.Generic;
using UnityEngine;

public abstract class GenericObjectPool<TProjectile> : MonoBehaviour where TProjectile : Projectile
{
    public static GenericObjectPool<TProjectile> Instance { get; private set; }

    List<(TProjectile projectile, Queue<TProjectile> queue)> objectPool = new List<(TProjectile, Queue<TProjectile>)>();

    void Awake()
    {
        Instance = this;
    }

    public void UpdatePoolableObjects(List<TProjectile> projectiles)
    {
        objectPool.Clear();

        for (int i = 0; i < projectiles.Count; i++)
        {
            objectPool.Add((projectiles[i], new Queue<TProjectile>()));
        }
    }

    public TProjectile Get(int index)
    {
        if (objectPool[index].queue.Count == 0) ExpandPool(index);
        return objectPool[index].queue.Dequeue();
    }

    public void ReturnToPool(TProjectile returningObject)
    {
        returningObject.gameObject.SetActive(false);
        returningObject.enabled = false;

        for (int i = 0; i < objectPool.Count; i++)
        {
            if (objectPool[i].projectile.GetType() == returningObject.GetType())
            {
                objectPool[i].queue.Enqueue(returningObject);
            }
        }
    }

    void ExpandPool(int index)
    {
        TProjectile newProjectile = Instantiate(objectPool[index].projectile, transform);
        newProjectile.gameObject.SetActive(false);

        objectPool[index].queue.Enqueue(newProjectile);
    }
}