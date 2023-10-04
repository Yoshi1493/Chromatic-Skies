using System.Collections;

public class TaurusBullet55 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 3f;
        yield return this.HomeInOn(playerShip, 2f, 1f);
    }
}