using System.Collections;
using static CoroutineHelper;

public class LibraBullet50 : EnemyBullet
{
    protected override float MaxLifetime => 20f;

    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(1f);

        float startSpeed = MoveSpeed;
        yield return this.LerpSpeed(startSpeed, 0f, 1f);

        yield return WaitForSeconds(1f);
        yield return this.LerpSpeed(0f, 2f, 5f);
    }
}