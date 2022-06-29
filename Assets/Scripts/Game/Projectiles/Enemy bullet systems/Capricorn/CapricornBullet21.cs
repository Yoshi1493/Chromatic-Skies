using System.Collections;

public class CapricornBullet21 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(1f, 2f, 0.5f);
    }
}