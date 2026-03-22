using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum CollectibleType
{
    Score = 0,
    Health = 1,
    Power = 2,
}

[Serializable]
public class CollectibleIntDictionary : SerializableDictionary<CollectibleType, int> { }

public class CollectibleObjectPool : GenericObjectPool<Collectible>
{
    readonly Dictionary<CollectibleType, Queue<Collectible>> collectiblesPool = new();

    [SerializeField] CollectibleIntDictionary prewarmDictionary;
    [SerializeField] List<Collectible> collectibles;

    void Start()
    {
        foreach (var item in prewarmDictionary.Keys)
        {
            int prewarmCount = prewarmDictionary[item];

            Queue<Collectible> tmp = new(prewarmCount);

            for (int ii = 0; ii < prewarmCount; ii++)
            {
                tmp.Enqueue(Get((int)item));
            }
            for (int ii = 0; ii < prewarmCount; ii++)
            {
                ReturnToPool(tmp.Dequeue(), (int)item);
            }
        }
    }

    public override void UpdatePoolableObjects()
    {
        for (int i = 0; i < collectibles.Count; i++)
        {
            if (Enum.IsDefined(typeof(CollectibleType), i))
            {
                collectiblesPool.Add((CollectibleType)i, new Queue<Collectible>());
            }
        }
    }

    public override Collectible Get(int collectibleID)
    {
        if (collectiblesPool[(CollectibleType)collectibleID].Count > 0)
        {
            return collectiblesPool[(CollectibleType)collectibleID].Dequeue();
        }
        else
        {
            Collectible newCollectible = Instantiate(collectibles[collectibleID], transform);
            Disable(newCollectible);

            return newCollectible;
        }
    }

    public override void ReturnToPool(Collectible returningCollectible, int collectibleID)
    {
        Disable(returningCollectible);
        collectiblesPool[(CollectibleType)collectibleID].Enqueue(returningCollectible);
    }

    protected override void Disable(Collectible collectible)
    {
        collectible.gameObject.SetActive(false);
        collectible.enabled = false;
    }

    public override void DrainPool()
    {
        base.DrainPool();
        collectiblesPool.Clear();
    }
}
