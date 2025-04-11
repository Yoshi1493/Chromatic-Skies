using System.Collections;

public class GeminiBullet11 : BossBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(5f, endSpeed, 0.5f);
    }
}