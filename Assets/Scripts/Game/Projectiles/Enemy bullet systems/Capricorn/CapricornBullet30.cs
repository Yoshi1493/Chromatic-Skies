using System.Collections;

public class CapricornBullet30 : EnemyBullet
{
    protected override float MaxLifetime => 7f;

    protected override IEnumerator Move()
    {
        float startSpeed = MoveSpeed;
        yield return this.LerpSpeed(startSpeed, 2f, 1f);
    }
}