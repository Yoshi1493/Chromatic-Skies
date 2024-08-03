using System.Collections;
using static CoroutineHelper;

public class LibraBulletSystem6 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 6;

    protected override float ShootingCooldown => 5f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 1; i <= WaveCount; i++)
            {
                SetSubsystemEnabled(i);
                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);
        }
    }
}