using System.Collections;

public class AquariusBullet21 : BossBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 2f, 1f);
    }
}