using System.Collections;

public class SagittariusBullet11 : BossBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 0f, 0.5f);
    }
}