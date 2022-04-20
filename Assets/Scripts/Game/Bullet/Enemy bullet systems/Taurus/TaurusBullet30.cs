using System.Collections;

public class TaurusBullet30 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 3f;
        yield return this.LerpSpeed(3f, 1.5f, 2f, 1f);
    }
}