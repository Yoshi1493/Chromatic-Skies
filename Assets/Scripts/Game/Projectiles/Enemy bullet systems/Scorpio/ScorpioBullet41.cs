using System.Collections;

public class ScorpioBullet41 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        float startSpeed = MoveSpeed;
        yield return this.LerpSpeed(startSpeed, 0f, 1f);
        yield return this.LerpSpeed(0f, startSpeed * 1.5f, 2f);
    }
}