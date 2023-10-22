using System.Collections;
using static CoroutineHelper;

public class CapricornBullet60 : EnemyBullet
{
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(-1f, 0f, 1f);
        yield return WaitForSeconds(2f);
        yield return this.LerpSpeed(0f, endSpeed, 1f);
    }
}