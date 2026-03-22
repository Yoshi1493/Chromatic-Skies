using System.Collections;

public class PiscesBullet03 : EnemyBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 0.5f, 0.5f);
        yield return this.LerpSpeed(0.5f, 3f, 1f);
    }
}