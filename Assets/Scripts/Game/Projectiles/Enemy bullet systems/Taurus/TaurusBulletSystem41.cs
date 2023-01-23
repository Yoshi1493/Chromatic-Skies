using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem41 : EnemyShooter<Laser>
{
    const int LaserCount = 15;
    const float MaxRotation = 20f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return WaitForSeconds(2f);

            float z = Random.Range(-MaxRotation, MaxRotation) + 180f;
            Vector3 pos = Vector3.zero;

            for (int i = 0; i < LaserCount; i++)
            {
                SpawnProjectile(0, z, pos).Fire();
                yield return WaitForSeconds(ShootingCooldown);
            }

            enabled = false;
        }
    }
}