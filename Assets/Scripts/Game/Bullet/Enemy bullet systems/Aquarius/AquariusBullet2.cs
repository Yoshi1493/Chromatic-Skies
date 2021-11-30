using System.Collections;
using static CoroutineHelper;

public class AquariusBullet2 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(2f);
        this.LookAt(playerShip.transform.position);
    }
}