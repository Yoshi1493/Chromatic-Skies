using System.Collections;

public class SagittariusBullet40 : BossBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 2f, 0.5f);
    }
}