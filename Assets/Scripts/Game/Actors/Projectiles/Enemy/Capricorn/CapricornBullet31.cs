using System.Collections;

public class CapricornBullet31 : BossBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(4f, 0f, 1f);
        yield return this.LerpSpeed(0f, endSpeed, 1f);
    }
}