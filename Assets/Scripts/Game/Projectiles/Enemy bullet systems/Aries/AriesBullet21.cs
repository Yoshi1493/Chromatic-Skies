using System.Collections;

public class AriesBullet21 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float startSpeed = MoveSpeed;
        yield return this.LerpSpeed(startSpeed, startSpeed * 0.5f, 1f);
    }
}