using System.Collections;

public class CapricornBullet06 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 0f, 1f);
    }
}