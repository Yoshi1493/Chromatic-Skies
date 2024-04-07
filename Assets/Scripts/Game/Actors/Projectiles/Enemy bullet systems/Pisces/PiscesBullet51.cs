using System.Collections;

public class PiscesBullet51 : EnemyBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(1f, 3f, 2f);
    }
}