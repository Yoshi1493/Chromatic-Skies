public class PSInvincibleBossShield : ParticleEffect
{
    protected override void ReturnToPool()
    {
        VFXObjectPool.Instance.ReturnToPool(this, (int)VFXType.InvincibleBossShield);
    }
}