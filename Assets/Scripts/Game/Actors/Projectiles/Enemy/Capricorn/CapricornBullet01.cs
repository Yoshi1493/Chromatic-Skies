using System.Collections;

public class CapricornBullet01 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(6f, 0f, 0.5f);
    }
}