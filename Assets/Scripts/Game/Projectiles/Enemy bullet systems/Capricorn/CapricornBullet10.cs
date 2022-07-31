using System.Collections;

public class CapricornBullet10 : EnemyBullet
{
    protected override float MaxLifetime => 4f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 3.2f, 0.5f);
    }
}