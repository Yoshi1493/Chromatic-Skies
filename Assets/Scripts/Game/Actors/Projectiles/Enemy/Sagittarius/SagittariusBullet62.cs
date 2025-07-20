using System.Collections;

public class SagittariusBullet62 : BossBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 2f, 1f);
    }
}