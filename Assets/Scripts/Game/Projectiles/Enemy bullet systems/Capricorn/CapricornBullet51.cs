using System.Collections;

public class CapricornBullet51 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(4.5f, 0f, 0.5f);
        yield return this.LerpSpeed(0f, endSpeed, 4f);
    }
}