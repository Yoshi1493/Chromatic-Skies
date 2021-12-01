using System.Collections;

public class AquariusBullet4 : EnemyBullet
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 1f, 2f);
    }
}