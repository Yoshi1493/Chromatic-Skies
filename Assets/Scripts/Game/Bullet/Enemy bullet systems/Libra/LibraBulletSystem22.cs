using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem22 : EnemyBulletSubsystem<EnemyBullet>
{
    const int BulletCount = 4;
    const int Spacing = 360 / BulletCount;
    const int Offset = 5;

    protected override float ShootingCooldown => 0.3f;

    protected override IEnumerator Shoot()
    {
        int i = 0;

        while (enabled)
        {
            for (int j = 0; j < BulletCount; j++)
            {
                float z = (i * Offset) + (j * Spacing);
                SpawnProjectile(1, z, Vector3.zero).Fire();

                z += 180f;
                SpawnProjectile(2, z, Vector3.zero).Fire();
            }

            i++;
            yield return WaitForSeconds(ShootingCooldown);
        }

    }
}