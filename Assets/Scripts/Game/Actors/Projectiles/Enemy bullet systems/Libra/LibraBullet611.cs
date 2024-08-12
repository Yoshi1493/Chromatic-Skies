using System.Collections;

public class LibraBullet611 : ScriptableEnemyBullet<LibraBulletSystem66, EnemyBullet>
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 3f, 0.5f);
        yield return this.LerpSpeed(3f, 0f, 0.5f);
        yield return this.LerpSpeed(0f, 3f, 3f);
    }
}