using System.Collections;
using static CoroutineHelper;

public class AquariusBullet50 : EnemyBullet
{
    protected override float MaxLifetime => base.MaxLifetime;

    protected override IEnumerator Move()
    {
        MoveSpeed = 5f;
        yield return WaitForSeconds(1f);

        yield return this.LerpSpeed(5f, 0f, 1f);

        if (playerShip.transform.position.y < transform.position.y)
        {
            this.LookAt(playerShip);
        }

        yield return WaitForSeconds(1f);
        yield return this.LerpSpeed(0f, 1f, 1f);
    }
}