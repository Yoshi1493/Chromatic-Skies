using System.Collections;
using static CoroutineHelper;

public class TaurusBullet51 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;

        yield return this.LerpSpeed(5f, 0f, 0.5f);
        yield return WaitForSeconds(2f);
        yield return this.LerpSpeed(0f, endSpeed, 1f);
    }
}