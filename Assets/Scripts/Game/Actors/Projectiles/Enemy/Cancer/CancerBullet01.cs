using System.Collections;
using static CoroutineHelper;

public class CancerBullet01 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float startSpeed = MoveSpeed;
        yield return this.LerpSpeed(startSpeed, 0f, 1f);
        yield return WaitForSeconds(0.5f);
        yield return this.LerpSpeed(0f, 2f, 2f);
    }
}