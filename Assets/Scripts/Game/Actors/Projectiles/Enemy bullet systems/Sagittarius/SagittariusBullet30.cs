using System.Collections;

public class SagittariusBullet30 : EnemyBullet
{
    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(0, endSpeed, 0.5f);
    }
}