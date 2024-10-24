using System.Collections;
using static CoroutineHelper;

public class TaurusBullet52 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float startSpeed = MoveSpeed;

        yield return this.LerpSpeed(0f, startSpeed, 0.5f);
        yield return this.LerpSpeed(MoveSpeed, 0f, 0.5f);

        yield return this.GraduallyLookAt(playerShip.transform.position, 0.5f);
        StartCoroutine(this.LerpSpeed(0f, 3f, 0.5f));

        yield return WaitForSeconds(1f);
        StartCoroutine(this.HomeInOn(playerShip, 0.5f));
    }
}