using System.Collections;

public class CapricornBullet30 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;
        yield return this.LerpSpeed(0f, 3f, 1f, 2f);
    }
}