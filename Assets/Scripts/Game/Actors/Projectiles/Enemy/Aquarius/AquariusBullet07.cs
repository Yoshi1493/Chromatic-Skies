using System.Collections;

public class AquariusBullet07 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float startSpeed = MoveSpeed;
        yield return this.LerpSpeed(startSpeed, 0f, 0.5f);
        yield return this.LerpSpeed(0f, startSpeed, 1f);
    }
}