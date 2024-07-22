using System.Collections;
using static CoroutineHelper;

public class ScorpioBullet31 : EnemyBullet
{
    protected override float MaxLifetime => 20f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(1f, 0f, 0.5f);
        yield return WaitForSeconds(0.5f);
        yield return this.LerpSpeed(0f, 1f, 4f);
    }
}