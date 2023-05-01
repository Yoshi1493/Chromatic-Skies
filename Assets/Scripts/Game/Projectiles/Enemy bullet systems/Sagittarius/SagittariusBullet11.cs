using System.Collections;

public class SagittariusBullet11 : EnemyBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 3f, 1f);
    }
}