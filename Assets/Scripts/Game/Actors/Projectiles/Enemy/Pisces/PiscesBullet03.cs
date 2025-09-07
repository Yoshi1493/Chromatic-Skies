using System.Collections;

public class PiscesBullet03 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 0f, 1f);
    }
}