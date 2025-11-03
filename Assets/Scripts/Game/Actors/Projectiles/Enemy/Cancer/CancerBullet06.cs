using System.Collections;
using static CoroutineHelper;

public class CancerBullet06 : MinibossBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(1f, 0f, 1f);
        yield return WaitForSeconds(2f);
        yield return this.LerpSpeed(0f, 2.5f, 1.5f);
    }
}