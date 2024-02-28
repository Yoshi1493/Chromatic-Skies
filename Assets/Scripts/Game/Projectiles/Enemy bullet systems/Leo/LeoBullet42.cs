using System.Collections;

public class LeoBullet42 : EnemyBullet
{
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        float startSpeed = MoveSpeed;
        float endSpeed = startSpeed * 0.1f;
        yield return this.LerpSpeed(startSpeed, endSpeed, 1f);
        yield return this.LerpSpeed(endSpeed, startSpeed, 2f);
    }
}