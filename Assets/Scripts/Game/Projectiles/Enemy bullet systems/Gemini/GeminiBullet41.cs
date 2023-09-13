using System.Collections;

public class GeminiBullet41 : EnemyBullet
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(0f, endSpeed * 2f, 1f);
        yield return this.LerpSpeed(MoveSpeed, endSpeed, 2f);
    }
}