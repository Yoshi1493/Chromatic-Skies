using System.Collections;

public class CapricornBullet09 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 0f, 0.5f);
    }
}