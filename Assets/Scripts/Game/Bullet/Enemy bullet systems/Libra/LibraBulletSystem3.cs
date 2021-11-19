using System.Collections;

public class LibraBulletSystem3 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
		while (enabled)
		{
			yield return null;
		}        
    }
}