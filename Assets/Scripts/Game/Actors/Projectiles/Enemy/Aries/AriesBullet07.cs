using System.Collections;

public class AriesBullet07 : MinibossBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 3.5f, 1.5f);
    }
}