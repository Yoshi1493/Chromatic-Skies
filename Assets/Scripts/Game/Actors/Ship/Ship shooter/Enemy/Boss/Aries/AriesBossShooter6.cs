using System.Collections;

public class AriesBossShooter6 : BossShooter<BossBullet>
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