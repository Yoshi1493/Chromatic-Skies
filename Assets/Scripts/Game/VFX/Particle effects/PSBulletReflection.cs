using System.Collections;
using UnityEngine;

public class PSBulletReflection : ParticleEffect
{
    protected override void ReturnToPool()
    {
        VFXObjectPool.Instance.ReturnToPool(gameObject, VFXType.BulletReflection);
    }
}