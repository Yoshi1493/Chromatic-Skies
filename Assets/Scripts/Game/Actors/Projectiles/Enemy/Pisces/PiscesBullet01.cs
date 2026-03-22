using System.Collections;
using static CoroutineHelper;

public class PiscesBullet01 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(1f);
        yield return this.LerpSpeed(3f, 1.5f, 1f);
    }
}