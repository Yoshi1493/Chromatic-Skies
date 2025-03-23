using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum CollectibleType
{
    Score = 0,
    Health = 1,
}

public class CollectibleObjectPool : GenericObjectPool
{
    readonly Dictionary<CollectibleType, Queue<GameObject>> collectiblesPool = new();
    [SerializeField] List<GameObject> collectibles;

    public override void UpdatePoolableObjects()
    {
        for (int i = 0; i < collectibles.Count; i++)
        {
            if (Enum.IsDefined(typeof(CollectibleType), i))
            {
                collectiblesPool.Add((CollectibleType)i, new Queue<GameObject>());
            }
        }
    }

    public override GameObject Get(int collectibleID)
    {
        if (collectiblesPool[(CollectibleType)collectibleID].Count > 0)
        {
            return collectiblesPool[(CollectibleType)collectibleID].Dequeue();
        }
        else
        {
            GameObject newCollectible = Instantiate(collectibles[collectibleID], transform);
            Disable(newCollectible);

            return newCollectible;
        }
    }

    public override void ReturnToPool(GameObject returningCollectible, int collectibleID)
    {
        Disable(returningCollectible);
        collectiblesPool[(CollectibleType)collectibleID].Enqueue(returningCollectible);
    }

    protected override void Disable(GameObject go)
    {
        base.Disable(go);
        go.GetComponent<Collectible>().enabled = false;
    }

    public override void DrainPool()
    {
        base.DrainPool();
        collectiblesPool.Clear();
    }
}
