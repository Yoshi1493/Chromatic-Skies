using System.Collections.Generic;
using UnityEngine;

public abstract class GenericBulletPool<TBullet> : MonoBehaviour where TBullet : Bullet
{
    public static GenericBulletPool<TBullet> Instance { get; private set; }

    List<(TBullet bullet, Queue<TBullet> queue)> bulletPool = new List<(TBullet bullet, Queue<TBullet> queue)>();

    void Awake()
    {
        Instance = this;
    }

    public void UpdatePoolableBullets(List<TBullet> bullets)
    {
        bulletPool.Clear();

        for (int i = 0; i < bullets.Count; i++)
        {
            bulletPool.Add((bullets[i], new Queue<TBullet>()));
        }
    }

    public TBullet Get(int index)
    {
        if (bulletPool[index].queue.Count == 0) ExpandPool(index);
        return bulletPool[index].queue.Dequeue();
    }

    public void ReturnToPool(int index, TBullet returningBullet)
    {
        returningBullet.gameObject.SetActive(false);
        bulletPool[index].queue.Enqueue(returningBullet);
    }

    void ExpandPool(int index)
    {
        TBullet newBullet = Instantiate(bulletPool[index].bullet, transform);
        newBullet.gameObject.SetActive(false);

        bulletPool[index].queue.Enqueue(newBullet);
    }
}