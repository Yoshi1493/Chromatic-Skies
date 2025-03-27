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
    InvincibleBossShield = 4
}

public class VFXObjectPool : GenericObjectPool<ParticleEffect>
{
    readonly Dictionary<VFXType, Queue<ParticleEffect>> vfxPool = new();
    [SerializeField] List<ParticleEffect> visualEffects;

    public override void UpdatePoolableObjects()
    {
        for (int i = 0; i < visualEffects.Count; i++)
        {
            if (Enum.IsDefined(typeof(VFXType), i))
            {
                vfxPool.Add((VFXType)i, new Queue<ParticleEffect>());
            }
        }
    }

    public override ParticleEffect Get(int vfxID)
    {
        if (vfxPool[(VFXType)vfxID].Count > 0)
        {
            return vfxPool[(VFXType)vfxID].Dequeue();
        }
        else
        {
            ParticleEffect newEffect = Instantiate(visualEffects[vfxID], transform);
            Disable(newEffect);

            return newEffect;
        }
    }

    public override void ReturnToPool(ParticleEffect returningEffect, int vfxID)
    {
        Disable(returningEffect);
        vfxPool[(VFXType)vfxID].Enqueue(returningEffect);
    }

    protected override void Disable(ParticleEffect effect)
    {
        effect.gameObject.SetActive(false);
        effect.enabled = false;
    }

    public override void DrainPool()
    {
        base.DrainPool();
        vfxPool.Clear();
    }
}