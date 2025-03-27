using System.Collections;

public class AquariusBullet20 : BossBullet
{
    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(6f, 2f, 1f);
    }
}