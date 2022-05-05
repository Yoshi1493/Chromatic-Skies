using System.Collections;

public class LeoBullet23 : EnemyBullet
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(0f, endSpeed, 2f);
    }
}