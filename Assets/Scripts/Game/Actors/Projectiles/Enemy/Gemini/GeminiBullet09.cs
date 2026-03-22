using System.Collections;

public class GeminiBullet09 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(1f, 3f, 1f);
    }
}