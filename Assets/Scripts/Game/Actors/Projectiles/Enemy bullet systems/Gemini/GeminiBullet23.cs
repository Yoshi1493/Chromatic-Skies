using System.Collections;

public class GeminiBullet23 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 2.5f, 1f);
    }
}