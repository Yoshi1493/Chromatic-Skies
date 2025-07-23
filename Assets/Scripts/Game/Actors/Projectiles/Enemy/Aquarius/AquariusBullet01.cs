using System.Collections;

public class AquariusBullet01 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(2f, 3f, 1f);
    }
}