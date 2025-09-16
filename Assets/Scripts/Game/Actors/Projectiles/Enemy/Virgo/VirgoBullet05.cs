using System.Collections;
using static CoroutineHelper;

public class VirgoBullet05 : MinibossBullet
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0.5f, 0f, 0.5f);
        yield return WaitForSeconds(1f);

        yield return this.LerpSpeed(0f, 1.5f, 1f);
    }
}