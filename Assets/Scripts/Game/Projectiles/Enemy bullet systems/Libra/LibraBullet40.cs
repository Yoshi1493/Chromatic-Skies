using System.Collections;

public class LibraBullet40 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 2f, 0.5f);
    }
}