public class PSBulletDestruction : ParticleEffect
{
    protected override void ReturnToPool()
    {
        VFXObjectPool.Instance.ReturnToPool(gameObject, VFXType.BulletDestruction);
    }
}