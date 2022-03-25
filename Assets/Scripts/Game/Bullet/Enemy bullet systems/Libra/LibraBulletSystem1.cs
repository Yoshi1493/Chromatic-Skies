using System.Collections;
using static CoroutineHelper;

public class LibraBulletSystem1 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);
        yield return WaitForSeconds(1f);
        SetSubsystemEnabled(2);
    }
}