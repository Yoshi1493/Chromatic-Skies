using System.Collections;
using static CoroutineHelper;

public class LibraBulletSystem12 : EnemyBulletSubsystem<EnemyBullet>
{
    const int MediumCount = 18;
    const int MediumSpacing = 360 / MediumCount;
    const int SmallCount = 30;
    const int SmallSpacing = 360 / SmallCount;
    const float Offset = -5f;

    protected override float ShootingCooldown => 0.8f;

    protected override IEnumerator Shoot()
    {
        int i = 0;

        while (enabled)
        {
            for (int j = 0; j < MediumCount; j++)
            {
                float z = (j * MediumSpacing) + (i * Offset);
                SpawnProjectile(1, z, transform.up.RotateVectorBy(z) * 0.1f).Fire();
            }

            i++;
            yield return WaitForSeconds(ShootingCooldown);

            for (int j = 0; j < SmallCount; j++)
            {
                float z = (j * SmallSpacing) + (i * Offset);
                SpawnProjectile(2, z, transform.up.RotateVectorBy(z) * 0.1f).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}