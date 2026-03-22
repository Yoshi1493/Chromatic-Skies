using System.Collections;

public class CapricornBullet05 : EnemyBullet
{
    protected override float MaxLifetime => 7f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 2.5f, 1f);
    }
}