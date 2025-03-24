public class PSBulletReflection : ParticleEffect
{
    protected override void ReturnToPool()
    {
        VFXObjectPool.Instance.ReturnToPool(this, (int)VFXType.BulletReflection);
    }
}