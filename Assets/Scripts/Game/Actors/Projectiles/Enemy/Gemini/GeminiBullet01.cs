using System.Collections;

public class GeminiBullet01 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 4f, 1f);
    }
}