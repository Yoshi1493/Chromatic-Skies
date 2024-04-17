using System.Collections;
using static CoroutineHelper;

public class PSPlayerRespawn : ParticleEffect
{
    protected override IEnumerator Play()
    {
        yield return WaitForSeconds(1f);
        yield return base.Play();
    }
}