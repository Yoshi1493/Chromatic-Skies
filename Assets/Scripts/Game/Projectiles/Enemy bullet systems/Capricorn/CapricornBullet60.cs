using System.Collections;
using static CoroutineHelper;

public class CapricornBullet60 : EnemyBullet
{
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(endSpeed, 0f, 0.5f);
        yield return WaitForSeconds(2f);
        yield return this.LerpSpeed(0f, -endSpeed, 1f);
    }
}