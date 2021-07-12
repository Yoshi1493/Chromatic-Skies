using System.Collections.Generic;
using UnityEngine;

public abstract class GenericProjectilePool<TProjectile> : MonoBehaviour where TProjectile : Projectile
{
    public static GenericProjectilePool<TProjectile> Instance { get; private set; }

    List<(TProjectile bullet, Queue<TProjectile> queue)> bulletPool = new List<(TProjectile bullet, Queue<TProjectile> queue)>();

    void Awake()
    {
        Instance = this;
    }

    public void UpdatePoolableBullets(List<TProjectile> bullets)
    {
        bulletPool.Clear();

        for (int i = 0; i < bullets.Count; i++)
        {
            bulletPool.Add((bullets[i], new Queue<TProjectile>()));
        }
    }

    public TProjectile Get(int index)
    {
        if (bulletPool[index].queue.Count == 0) ExpandPool(index);
        return bulletPool[index].queue.Dequeue();
    }

    public void ReturnToPool(int index, TProjectile returningBullet)
    {
        returningBullet.gameObject.SetActive(false);
        bulletPool[index].queue.Enqueue(returningBullet);
    }

    void ExpandPool(int index)
    {
        TProjectile newBullet = Instantiate(bulletPool[index].bullet, transform);
        newBullet.gameObject.SetActive(false);

        bulletPool[index].queue.Enqueue(newBullet);
    }
}