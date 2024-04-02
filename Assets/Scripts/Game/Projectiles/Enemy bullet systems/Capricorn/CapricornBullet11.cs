using System.Collections;

public class CapricornBullet11 : EnemyBullet
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 2f, 2f);
    }
}