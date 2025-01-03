using System.Collections;
using static CoroutineHelper;

public class CapricornBullet22 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;

        yield return this.LerpSpeed(4f, 0f, 1f);
        yield return WaitForSeconds(0.5f);
        yield return this.LerpSpeed(0f, endSpeed, 1f);
    }
}