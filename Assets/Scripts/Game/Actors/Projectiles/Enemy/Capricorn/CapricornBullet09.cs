using System.Collections;

public class CapricornBullet09 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0.5f, 4f, 2f);
    }
}