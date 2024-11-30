using System.Collections;

public class SpecialRed : SpecialBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(2f, 7f, 2f);
    }
}