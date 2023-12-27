using System.Collections;
using static CoroutineHelper;

public class GeminiBullet41 : EnemyBullet
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(1f);
        yield return this.LerpSpeed(0f, 2f, 2f);
    }
}