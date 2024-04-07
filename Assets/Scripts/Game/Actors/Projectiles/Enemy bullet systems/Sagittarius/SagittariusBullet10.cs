using System.Collections;

public class SagittariusBullet10 : EnemyBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 0f, 1f);
        yield return this.LerpSpeed(0f, 5f, 2.5f);
    }
}