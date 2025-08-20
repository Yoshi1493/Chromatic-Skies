public class PSPlayerGraze : ParticleEffect
{
    protected override void OnEnable()
    {
        base.OnEnable();
        PlaySound();
    }

    void PlaySound()
    {
        AudioManager.Instance.PlaySound("projectile_graze", true);
    }

    protected override void ReturnToPool()
    {
        VFXObjectPool.Instance.ReturnToPool(this, (int)VFXType.PlayerGraze);
    }


}