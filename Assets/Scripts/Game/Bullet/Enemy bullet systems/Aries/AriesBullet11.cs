using System.Collections;

public class AriesBullet11 : EnemyBullet
{
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 1.5f, 1f);
    }
}