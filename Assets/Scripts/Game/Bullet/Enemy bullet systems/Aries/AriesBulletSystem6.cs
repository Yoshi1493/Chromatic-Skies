using System.Collections;

public class AriesBulletSystem6 : EnemyShooter<EnemyBullet>
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