using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum VFXType
{
    PlayerGraze = 0,
    BulletDestruction = 1,
    BulletReflection = 2,
    InvinciblePlayerShield = 3,
    InvincibleEnemyShield = 4
}

public class VFXObjectPool : GenericObjectPool
{
    readonly Dictionary<VFXType, Queue<GameObject>> vfxPool = new();
    [SerializeField] List<GameObject> visualEffects;

    public override void UpdatePoolableObjects()
    {
        for (int i = 0; i < visualEffects.Count; i++)
        {
            if (Enum.IsDefined(typeof(VFXType), i))
            {
                vfxPool.Add((VFXType)i, new Queue<GameObject>());
            }
        }
    }

    public override GameObject Get(int vfxID)
    {
        if (vfxPool[(VFXType)vfxID].Count > 0)
        {
            return vfxPool[(VFXType)vfxID].Dequeue();
        }
        else
        {
            GameObject newEffect = Instantiate(visualEffects[vfxID], transform);
            Disable(newEffect);

            return newEffect;
        }
    }

    public override void ReturnToPool(GameObject returningEffect, int vfxID)
    {
        Disable(returningEffect);
        vfxPool[(VFXType)vfxID].Enqueue(returningEffect);
    }

    protected override void Disable(GameObject go)
    {
        base.Disable(go);
        go.GetComponent<ParticleEffect>().enabled = false;
    }

    public override void DrainPool()
    {
        base.DrainPool();
        vfxPool.Clear();
    }
}