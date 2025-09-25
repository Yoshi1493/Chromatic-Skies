using System.Collections;
using static CoroutineHelper;

public class VirgoBullet08 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 0f, 0.5f);
        yield return this.LerpSpeed(0f, -2f, 1.5f);
    }
}