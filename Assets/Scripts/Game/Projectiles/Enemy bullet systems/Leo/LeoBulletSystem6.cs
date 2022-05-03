using System.Collections;

public class LeoBulletSystem6 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
		while (enabled)
		{
			yield return null;
		}        
    }
}