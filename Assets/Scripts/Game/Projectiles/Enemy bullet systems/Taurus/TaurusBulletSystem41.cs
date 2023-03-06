using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem41 : EnemyShooter<Laser>
{
    const int LaserCount = 15;
    const float SpawnAngleVariance = 20f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3f);

        float r = Random.Range(-SpawnAngleVariance, SpawnAngleVariance);
        Vector3 pos = Vector3.zero;

        for (int i = 0; i < LaserCount; i++)
        {
            float z = 180f + r;

            SpawnProjectile(0, z, pos).Fire();
            yield return WaitForSeconds(ShootingCooldown);
        }

        yield return WaitForSeconds(0.5f);

        for (int i = 0; i < LaserCount; i++)
        {
            float z = 180f - r;

            SpawnProjectile(0, z, pos).Fire();
            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}