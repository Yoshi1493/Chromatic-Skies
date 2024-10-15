using System.Collections;
using static CoroutineHelper;

public class PSInvincibleEnemyShield : ParticleEffect
{
    protected override IEnumerator Play()
    {
        yield return base.Play();

        float particleLifetime = ParticleSystem.GetFloat("ParticleLifetime");
        yield return WaitForSeconds(particleLifetime);

        VFXObjectPool.Instance.ReturnToPool(gameObject, VFXType.InvincibleEnemyShield);
    }
}