using System.Collections;
using static CoroutineHelper;

public class CapricornBullet41 : EnemyBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return WaitForSeconds(1f);
        yield return this.LerpSpeed(0f, endSpeed, 1f);
    }
}