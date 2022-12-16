using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem12 : EnemyShooter<EnemyBullet>
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
                Vector3 pos = transform.up.RotateVectorBy(z) * 0.1f;

                SpawnProjectile(1, z, pos).Fire();
            }

            i++;
            yield return WaitForSeconds(ShootingCooldown);

            for (int j = 0; j < SmallCount; j++)
            {
                float z = (j * SmallSpacing) + (i * Offset);
                Vector3 pos = transform.up.RotateVectorBy(z) * 0.1f;

                SpawnProjectile(2, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}