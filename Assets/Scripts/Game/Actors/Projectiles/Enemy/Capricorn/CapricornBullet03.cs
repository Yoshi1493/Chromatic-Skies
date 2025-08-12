using System.Collections;

public class CapricornBullet03 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(0f, endSpeed, 1f);
    }
}