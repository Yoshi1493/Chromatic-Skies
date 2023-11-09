using System.Collections;

public class PiscesBullet21 : EnemyBullet
{
    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 3f, 1f);
    }
}