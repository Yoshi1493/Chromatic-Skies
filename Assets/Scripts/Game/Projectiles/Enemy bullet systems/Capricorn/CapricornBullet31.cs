using System.Collections;

public class CapricornBullet31 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(endSpeed * 2f, endSpeed, 1f);
    }
}