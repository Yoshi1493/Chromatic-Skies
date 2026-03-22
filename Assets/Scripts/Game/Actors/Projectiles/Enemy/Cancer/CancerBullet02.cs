using System.Collections;
using static CoroutineHelper;

public class CancerBullet02 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 1f, 1f);
        yield return WaitForSeconds(0.5f);

        yield return this.LerpSpeed(4f, 3f, 1f);
    }
}