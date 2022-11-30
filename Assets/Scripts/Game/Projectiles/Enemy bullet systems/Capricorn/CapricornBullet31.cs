using System.Collections;
using static CoroutineHelper;

public class CapricornBullet31 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 0f, 1f);
        yield return WaitForSeconds(0.5f);
        MoveSpeed = 3f;
    }
}