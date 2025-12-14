using System.Collections;

public class GeminiBullet04 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(2f, 4f, 1.5f);
    }
}