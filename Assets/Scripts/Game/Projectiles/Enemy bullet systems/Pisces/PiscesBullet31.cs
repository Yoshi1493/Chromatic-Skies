using System.Collections;

public class PiscesBullet31 : EnemyBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(6f, 0.5f, 1f);
        yield return this.LerpSpeed(0.5f, endSpeed, 1f);
    }
}