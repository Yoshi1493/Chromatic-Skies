using System.Collections;

public class LeoBullet61 : EnemyBullet
{
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(-endSpeed * 2f, endSpeed, 2f);
    }
}