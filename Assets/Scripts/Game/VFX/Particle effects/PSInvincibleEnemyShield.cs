public class PSInvincibleEnemyShield : ParticleEffect
{
    protected override void ReturnToPool()
    {
        VFXObjectPool.Instance.ReturnToPool(this, (int)VFXType.InvincibleEnemyShield);
    }
}