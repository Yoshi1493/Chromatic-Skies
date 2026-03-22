using System.Collections;

public class VirgoBullet04 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(2f, 4f, 1f);
        yield return this.LerpSpeed(4f, 2f, 1f);
    }
}