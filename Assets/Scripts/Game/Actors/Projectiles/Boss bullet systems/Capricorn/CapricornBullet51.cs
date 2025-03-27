using System.Collections;

public class CapricornBullet51 : BossBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(1f, endSpeed, 0.5f);
    }
}