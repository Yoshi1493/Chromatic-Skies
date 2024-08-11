using System.Collections;

public class LibraBullet610 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 0f, 1f);
    }
}