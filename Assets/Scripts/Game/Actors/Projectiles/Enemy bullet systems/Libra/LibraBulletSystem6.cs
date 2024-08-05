using System.Collections;
using static CoroutineHelper;

public class LibraBulletSystem6 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 6;

    protected override float ShootingCooldown => 10f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            SetSubsystemEnabled(1);
            SetSubsystemEnabled(2);
            //SetSubsystemEnabled(3);
            //SetSubsystemEnabled(4);
            //SetSubsystemEnabled(5);
            //SetSubsystemEnabled(6);

            //for (int i = 1; i <= WaveCount; i++)
            //{
            //    SetSubsystemEnabled(i);
            //    yield return WaitForSeconds(ShootingCooldown);
            //}

            yield return WaitForSeconds(100f);
        }
    }
}