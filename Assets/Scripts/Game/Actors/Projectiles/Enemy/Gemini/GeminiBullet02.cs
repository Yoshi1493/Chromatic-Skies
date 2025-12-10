using System.Collections;
using static CoroutineHelper;

public class GeminiBullet02 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(-0.1f, 0f, 0.5f);
        yield return WaitForSeconds(1f);
        yield return this.LerpSpeed(0f, 2.5f, 1f);
    }
}