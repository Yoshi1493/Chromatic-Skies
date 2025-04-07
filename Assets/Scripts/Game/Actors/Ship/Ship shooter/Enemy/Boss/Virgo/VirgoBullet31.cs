using System.Collections;

public class VirgoBullet31 : BossBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 4f, 1f);
    }
}