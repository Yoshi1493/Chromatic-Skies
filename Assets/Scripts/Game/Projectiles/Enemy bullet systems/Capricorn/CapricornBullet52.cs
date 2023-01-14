using System.Collections;

public class CapricornBullet52 : EnemyBullet
{
    protected override float MaxLifetime => 7f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(endSpeed, 0f, 0.5f);
        yield return this.LerpSpeed(0f, endSpeed, 1f);
    }
}