using System.Collections;

public class LibraBullet10 : EnemyBullet
{
    protected override float MaxLifetime => 4f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(2f, 0f, 0.5f);
        yield return this.LerpSpeed(0f, 4f, 1f);
    }
}