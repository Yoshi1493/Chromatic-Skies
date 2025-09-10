using System.Collections;
using static CoroutineHelper;

public class PiscesBullet010 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(1f);
        yield return this.LerpSpeed(0f, 2.5f, 1f);
    }
}