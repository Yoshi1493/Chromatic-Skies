using System.Collections;

public class VirgoBullet00 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 1f, 1f);
        yield return this.LerpSpeed(1f, 3f, 2f);
    }
}