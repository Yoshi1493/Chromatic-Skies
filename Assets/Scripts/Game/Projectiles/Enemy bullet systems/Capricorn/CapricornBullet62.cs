using System.Collections;

public class CapricornBullet62 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(0f, endSpeed, 0.5f);
    }
}