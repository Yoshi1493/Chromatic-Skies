using System.Collections;

public class CapricornBullet50 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 3f, 1f);
    }
}