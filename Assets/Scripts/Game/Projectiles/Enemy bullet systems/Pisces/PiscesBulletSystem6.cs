using System.Collections;

public class PiscesBulletSystem6 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
		while (enabled)
		{
			yield return null;
		}        
    }
}