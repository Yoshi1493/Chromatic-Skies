using System.Collections;

public class TaurusBullet40 : BossBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(2f, 4f, 3f);
    }
}