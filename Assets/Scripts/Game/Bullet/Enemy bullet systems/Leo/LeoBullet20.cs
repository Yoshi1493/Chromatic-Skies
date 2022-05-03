using System.Collections;

public class LeoBullet20 : EnemyBullet
{
    protected override float MaxLifetime => 2f;

    protected override IEnumerator Move()
    {
        float startSpeed = MoveSpeed;
        yield return this.LerpSpeed(startSpeed, 0f, 1f);
    }
}