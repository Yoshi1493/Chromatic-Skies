using System.Collections;

public class TaurusBulletSystem4 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
		while (enabled)
		{
			yield return null;
		}        
    }
}