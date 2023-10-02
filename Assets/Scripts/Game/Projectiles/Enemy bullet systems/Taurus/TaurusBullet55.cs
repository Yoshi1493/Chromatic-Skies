using System.Collections;

public class TaurusBullet55 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.HomeInOn(playerShip, 1f, 1f);
    }
}