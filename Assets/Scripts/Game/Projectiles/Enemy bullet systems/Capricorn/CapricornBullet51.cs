using System.Collections;

public class CapricornBullet51 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 0f, 0.5f);
        yield return this.LerpSpeed(0f, 4f, 4f);
    }
}