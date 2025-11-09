using System.Collections;

public class TaurusBullet00 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 3f, 1f);
    }
}