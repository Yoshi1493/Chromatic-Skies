using System.Collections;

public class TaurusBullet20 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 3f, 1f, 1f);
    }
}