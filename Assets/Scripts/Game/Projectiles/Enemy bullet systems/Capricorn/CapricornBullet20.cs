using System.Collections;

public class CapricornBullet20 : EnemyBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(10f, 2f, 0.5f);
    }
}