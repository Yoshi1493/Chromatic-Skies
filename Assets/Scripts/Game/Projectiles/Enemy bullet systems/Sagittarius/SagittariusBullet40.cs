using System.Collections;

public class SagittariusBullet40 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 2f, 0.5f);
    }
}