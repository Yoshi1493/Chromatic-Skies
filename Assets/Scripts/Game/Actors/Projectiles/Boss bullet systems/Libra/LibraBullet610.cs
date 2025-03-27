using System.Collections;

public class LibraBullet610 : BossBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 0f, 1f);
    }
}