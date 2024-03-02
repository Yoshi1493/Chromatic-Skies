using System.Collections;

public class LeoBullet30 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(endSpeed * 2f, 0.5f, 0.5f);
        yield return this.LerpSpeed(0.5f, endSpeed, 2f);
    }
}