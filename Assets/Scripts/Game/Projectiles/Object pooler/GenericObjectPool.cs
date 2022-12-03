using System.Collections.Generic;
using UnityEngine;

public abstract class GenericObjectPool<TProjectile> : MonoBehaviour where TProjectile : Projectile
{
    public static GenericObjectPool<TProjectile> Instance { get; private set; }

    readonly List<(TProjectile projectile, Queue<TProjectile> queue)> objectPool = new();

    void Awake()
    {
        Instance = this;
    }

    public void UpdatePoolableObjects(List<TProjectile> projectiles)
    {
        for (int i = 0; i < projectiles.Count; i++)
        {
            objectPool.Add((projectiles[i], new Queue<TProjectile>()));
        }
    }

    public void DrainPool()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        objectPool.Clear();
    }

    public TProjectile Get(int ID)
    {
        if (objectPool[ID].queue.Count > 0)
        {
            return objectPool[ID].queue.Dequeue();
        }
        else
        {
            TProjectile newProjectile = Instantiate(objectPool[ID].projectile, transform);
            newProjectile.enabled = false;

            return newProjectile;
        }
    }

    public void ReturnToPool(TProjectile returningObject)
    {
        returningObject.gameObject.SetActive(false);
        returningObject.enabled = false;

        objectPool[returningObject.ProjectileID].queue.Enqueue(returningObject);
    }
}