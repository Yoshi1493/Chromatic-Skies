using System.Collections;
using static CoroutineHelper;

public class CancerBullet08 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 0f, 0.5f);
        yield return WaitForSeconds(1f);
        yield return this.LerpSpeed(2f, 3f, 1f);
    }
}