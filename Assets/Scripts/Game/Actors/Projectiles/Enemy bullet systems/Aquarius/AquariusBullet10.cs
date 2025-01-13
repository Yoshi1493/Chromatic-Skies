using System.Collections;

public class AquariusBullet10 : EnemyBullet
{
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        float startSpeed = MoveSpeed;
        yield return this.LerpSpeed(startSpeed, startSpeed * 0.4f, 3f);
    }
}