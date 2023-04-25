using System.Collections;

public class LibraBullet50 : EnemyBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(MoveSpeed, endSpeed * 2f, 3f);
    }
}