using System.Collections;

public class VirgoBullet21 : BossBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 2f, 1f);
    }
}