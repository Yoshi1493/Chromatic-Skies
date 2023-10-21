using System.Collections;

public class CapricornBullet62 : EnemyBullet
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(5f, 0f, 0.5f);
        yield return this.LerpSpeed(0f, endSpeed, 1f);
    }
}