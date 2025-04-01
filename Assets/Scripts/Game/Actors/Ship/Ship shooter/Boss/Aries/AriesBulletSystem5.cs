using System.Collections;

public class AriesBulletSystem5 : BossShooter<BossBullet>
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