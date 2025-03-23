public class PSBulletReflection : ParticleEffect
{
    protected override void ReturnToPool()
    {
        VFXObjectPool.Instance.ReturnToPool(gameObject, (int)VFXType.BulletReflection);
    }
}