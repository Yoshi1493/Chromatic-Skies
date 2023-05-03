using System.Collections;

public class SagittariusBullet20 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed * 0.5f;
        yield return this.LerpSpeed(MoveSpeed, endSpeed, MaxLifetime * 0.5f);
    }
}