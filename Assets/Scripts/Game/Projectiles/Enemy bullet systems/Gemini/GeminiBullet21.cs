using System.Collections;

public class GeminiBullet21 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(0f, endSpeed, 1f);
    }
}