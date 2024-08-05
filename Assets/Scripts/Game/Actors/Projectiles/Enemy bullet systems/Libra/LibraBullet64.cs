using System.Collections;
using static CoroutineHelper;

public class LibraBullet64 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 0f, 1f);

        yield return WaitForSeconds(1f);
        yield return this.LerpSpeed(0f, 2f, 2f);
    }
}