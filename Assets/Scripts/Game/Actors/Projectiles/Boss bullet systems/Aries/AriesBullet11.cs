using System.Collections;

public class AriesBullet11 : BossBullet
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 1.5f, 1f);
    }
}