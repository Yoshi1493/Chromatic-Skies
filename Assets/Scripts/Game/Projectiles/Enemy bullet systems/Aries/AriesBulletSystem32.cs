using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem32 : EnemyShooter<Laser>
{
    const int LaserCount = 21;
    const float MinAngle = 5f;
    const float MaxAngle = 85f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < LaserCount; i++)
        {
            float l = i / (LaserCount - 1f);
            float z = Mathf.Lerp(MaxAngle, MinAngle, l) + 180f;
            Vector3 pos = 6f * Vector3.up;

            bulletData.colour = bulletData.gradient.Evaluate(l);
            SpawnProjectile(0, z, pos).Fire();
            SpawnProjectile(0, -z, pos).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}