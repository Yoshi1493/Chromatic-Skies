using System.Collections;
using static CoroutineHelper;

public class LeoBullet42 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(1f);

        float startSpeed = MoveSpeed;
        yield return this.LerpSpeed(startSpeed, 2f, 2f);
    }
}