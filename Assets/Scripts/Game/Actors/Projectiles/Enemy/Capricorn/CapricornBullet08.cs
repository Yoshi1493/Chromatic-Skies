using System.Collections;

public class CapricornBullet08 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(6f, 2f, 0.5f);
    }
}