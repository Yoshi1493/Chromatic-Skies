using System.Collections;

public class CapricornBullet10 : BossBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 0f, 0.5f);
        yield return this.LerpSpeed(0f, 2f, 1f);
    }
}