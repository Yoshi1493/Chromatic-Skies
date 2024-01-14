using System.Collections;

public class SagittariusBullet50 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(-8f, 0f, 1f);
        yield return this.LerpSpeed(0f, endSpeed, 1f);
        yield return this.LerpSpeed(endSpeed, 0f, 1f);
    }
}