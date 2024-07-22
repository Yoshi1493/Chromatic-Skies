using System.Collections;
using static CoroutineHelper;

public class LibraBullet31 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 5f, 0.5f);
        yield return this.LerpSpeed(5f, 0f, 0.5f);
        yield return WaitForSeconds(0.5f);

        this.LookAt(playerShip);
        yield return this.LerpSpeed(2f, 3f, 1f);
    }
}