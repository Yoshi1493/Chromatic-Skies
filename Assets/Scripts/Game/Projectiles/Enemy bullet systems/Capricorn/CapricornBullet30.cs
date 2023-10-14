using System.Collections;

public class CapricornBullet30 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(1f, endSpeed, 1f);
    }
}