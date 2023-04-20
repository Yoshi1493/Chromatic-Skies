using System.Collections;

public class LibraBullet40 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;
        yield return this.LerpSpeed(0f, 2.5f, 1f);
    }
}