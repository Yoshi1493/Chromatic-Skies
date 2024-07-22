using System.Collections;

public class VirgoBullet62 : EnemyBullet
{
    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(1f, endSpeed, 0.2f);
    }
}