using System.Collections;

public class TaurusBullet50 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float moveSpeed = MoveSpeed;
        yield return this.LerpSpeed(0, moveSpeed / -4f, 0.5f);
        yield return this.LerpSpeed(MoveSpeed, moveSpeed, 2f);
    }
}