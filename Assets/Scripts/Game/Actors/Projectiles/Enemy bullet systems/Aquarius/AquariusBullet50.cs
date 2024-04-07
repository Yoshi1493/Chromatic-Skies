using System.Collections;

public class AquariusBullet50 : EnemyBullet
{
    protected override float MaxLifetime => 1.5f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(8f, 3f, MaxLifetime);
    }
}