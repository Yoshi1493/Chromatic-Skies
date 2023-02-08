using System.Collections;

public class LibraBullet20 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(5f, endSpeed, 1f);
    }
}