using System.Collections;

public class CapricornBullet10 : EnemyBullet
{
    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 2.5f, 0.5f);
    }
}