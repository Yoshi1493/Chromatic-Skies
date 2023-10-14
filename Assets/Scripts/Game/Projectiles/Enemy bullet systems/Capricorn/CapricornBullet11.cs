using System.Collections;

public class CapricornBullet11 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 3f;
        yield return this.LerpSpeed(3f, 2f, 1f);
    }
}