using System.Collections;

public class CapricornBullet52 : EnemyBullet
{
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(7f, 1f, 1f);
    }
}