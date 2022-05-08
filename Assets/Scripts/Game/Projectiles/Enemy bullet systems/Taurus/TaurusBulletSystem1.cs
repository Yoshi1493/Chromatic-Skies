using System.Collections;
using static CoroutineHelper;

public class TaurusBulletSystem1 : EnemyShooter<EnemyBullet>
{
	protected override IEnumerator Shoot()
	{
		SetSubsystemEnabled(1);

		while (enabled)
		{
			yield return WaitForSeconds(ShootingCooldown);
		}
	}
}