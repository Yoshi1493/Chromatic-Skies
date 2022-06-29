using System.Collections;

public class CapricornBullet20 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 2f, 0.5f);
    }
}