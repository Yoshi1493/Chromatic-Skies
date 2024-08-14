using System.Collections;
using static CoroutineHelper;

public class LibraBulletSystem6 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 6;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            SetSubsystemEnabled(1);
            yield return WaitForSeconds(12f);

            SetSubsystemEnabled(2);
            yield return WaitForSeconds(12f);

            SetSubsystemEnabled(3);
            yield return WaitForSeconds(14f);

            SetSubsystemEnabled(4);
            yield return WaitForSeconds(12f);

            SetSubsystemEnabled(5);
            yield return WaitForSeconds(12f);

            SetSubsystemEnabled(6);
            yield return WaitForSeconds(12f);

        }
    }
}