using System.Collections;

public class TaurusBullet40 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 3f;
        yield return this.LerpSpeed(3f, 1.5f, 2f, 1f);
    }
}