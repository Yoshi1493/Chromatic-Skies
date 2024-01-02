using System.Collections;

public class VirgoBullet11 : EnemyBullet
{
    protected override float MaxLifetime => 7f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(1f, 3f, 1f);
    }
}