using System.Collections;

public class CapricornBullet05 : EnemyBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 2f, 1f);
    }
}