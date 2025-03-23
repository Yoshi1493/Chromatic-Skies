public class PSInvincibleEnemyShield : ParticleEffect
{
    protected override void ReturnToPool()
    {
        VFXObjectPool.Instance.ReturnToPool(gameObject, (int)VFXType.InvincibleEnemyShield);
    }
}