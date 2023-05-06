using System.Collections;

public class SagittariusBullet22 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float startSpeed = MoveSpeed;
        yield return this.LerpSpeed(startSpeed, 2f, 1f);
    }
}