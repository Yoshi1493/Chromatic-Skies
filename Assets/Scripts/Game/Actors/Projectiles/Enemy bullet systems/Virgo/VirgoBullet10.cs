using System.Collections;

public class VirgoBullet10 : EnemyBullet
{
    protected override float MaxLifetime => 7f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 1f, 0.5f);
        yield return this.LerpSpeed(1f, 5f, 5f);
    }
}