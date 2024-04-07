using System.Collections;

public class AriesBulletSystem5 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

		while (enabled)
        {
			yield return null;
		}        
    }
}