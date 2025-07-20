using System.Collections;
using static CoroutineHelper;

public class AquariusBullet02 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 0f, 0.5f);
        yield return WaitForSeconds(0.5f);
        yield return this.LerpSpeed(0f, 2.5f, 1f);
    }
}