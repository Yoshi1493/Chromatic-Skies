using System.Collections;

public class AriesBossShooter5 : BossShooter<BossBullet>
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