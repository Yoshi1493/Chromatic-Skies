using System.Collections.Generic;
using UnityEngine;

public abstract class GenericObjectPool<TProjectile> : MonoBehaviour where TProjectile : Projectile
{
    public static GenericObjectPool<TProjectile> Instance { get; private set; }

    const int PoolCap = 256;
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
            objectPool.Add((projectiles[i], new Queue<TProjectile>(PoolCap)));

            //for (int j = 0; j < PoolLimit; j++)
            //{
            //    ExpandPool(i);
            //}
        }
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
            newProjectile.gameObject.SetActive(false);
            newProjectile.enabled = false;

            return newProjectile;
        }
    }

    public void ReturnToPool(TProjectile returningObject)
    {
        returningObject.gameObject.SetActive(false);
        returningObject.enabled = false;

        objectPool[returningObject.projectileData.BulletID].queue.Enqueue(returningObject);
    }
}