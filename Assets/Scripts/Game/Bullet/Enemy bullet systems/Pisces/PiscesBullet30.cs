using System.Collections;

public class PiscesBullet30 : EnemyBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 2f;

        yield return this.LerpSpeed(2, 0.5f, 0.5f);
        yield return this.LerpSpeed(MoveSpeed, 3f, 1f);
    }
}