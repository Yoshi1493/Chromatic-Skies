public class PSPlayerGraze : ParticleEffect
{
    protected override void ReturnToPool()
    {
        VFXObjectPool.Instance.ReturnToPool(gameObject, (int)VFXType.PlayerGraze);
    }
}