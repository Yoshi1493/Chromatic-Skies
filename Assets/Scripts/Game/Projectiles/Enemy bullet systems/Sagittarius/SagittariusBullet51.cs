using System.Collections;

public class SagittariusBullet51 : EnemyBullet
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 2f, 2f);
    }
}