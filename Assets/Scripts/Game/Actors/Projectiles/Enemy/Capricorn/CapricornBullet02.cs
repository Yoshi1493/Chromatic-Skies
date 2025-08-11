using System.Collections;
using static CoroutineHelper;

public class CapricornBullet02 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(1f, 0f, 0.5f);
        yield return WaitForSeconds(0.5f);
        yield return this.LerpSpeed(0f, 3f, 1f);
    }
}