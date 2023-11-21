using System.Collections;
using static CoroutineHelper;

public class CancerBullet31 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;
        yield return WaitForSeconds(2f);
        yield return this.LerpSpeed(0f, 2f, 2f);
    }
}