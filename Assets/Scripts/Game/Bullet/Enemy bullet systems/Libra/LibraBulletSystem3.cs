using System.Collections;

public class LibraBulletSystem3 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
		yield return base.Shoot();

		SetSubsystemEnabled(1);
		SetSubsystemEnabled(2);
    }
}