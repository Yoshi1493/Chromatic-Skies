using System.Collections;
using static CoroutineHelper;

public class PSProjectileDestruction : ParticleEffect
{
    protected override IEnumerator Play()
    {
        yield return base.Play();

        //necessary to bypass the latency that exists when checking ParticleSystem.aliveParticleCount (wtf??)
        while (ParticleSystem.aliveParticleCount == 0)
        {
            yield return null;
        }

        yield return WaitUntil(() => ParticleSystem.aliveParticleCount == 0);
        VFXObjectPool.Instance.ReturnToPool(gameObject, VFXType.ProjectileDestruction);
    }
}