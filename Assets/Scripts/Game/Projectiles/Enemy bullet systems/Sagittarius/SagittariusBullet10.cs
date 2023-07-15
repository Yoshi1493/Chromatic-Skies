using System.Collections;

public class SagittariusBullet10 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 2f, 1f);
    }
}