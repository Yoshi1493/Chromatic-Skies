using System.Collections;

public class AquariusBullet11 : EnemyBullet
{
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, -3f, 2f);
        yield return this.LerpSpeed(-3f, -1.5f, 4f);
    }
}