using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const float WaveSpacing = 5f;
    const int BulletCount = 12;
    const float BulletSpacing = 360f / BulletCount;

    protected override float ShootingCooldown => 0.25f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        int i = 0;

        while (enabled)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (i * WaveSpacing) + (ii * BulletSpacing);
                Vector3 pos = Vector3.zero;

                SpawnProjectile(0, z, pos).Fire();
                SpawnProjectile(1, -z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
            i++;
        }
    }
}