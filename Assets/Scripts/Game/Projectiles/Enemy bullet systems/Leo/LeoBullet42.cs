using System.Collections;

public class LeoBullet42 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(endSpeed, 0f, 1f);
        yield return this.LerpSpeed(0f, endSpeed, 2f);
    }
}