using System.Collections;

public class GeminiBullet00 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 2f, 1f);
    }
}