using System.Collections;

public class AquariusBullet51 : BossBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(10f, 2f, 1f);
    }
}