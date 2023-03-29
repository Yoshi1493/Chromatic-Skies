using System.Collections;
using static CoroutineHelper;

public class CancerBullet41 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(1f);
        StartCoroutine(this.LerpSpeed(0f, 3f, 1f));
        yield return this.HomeInOn(playerShip, 0.5f);
    }
}