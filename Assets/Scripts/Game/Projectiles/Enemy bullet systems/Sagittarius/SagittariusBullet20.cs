using System.Collections;

public class SagittariusBullet20 : EnemyBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(2f, 4f, 1f);
    }
}