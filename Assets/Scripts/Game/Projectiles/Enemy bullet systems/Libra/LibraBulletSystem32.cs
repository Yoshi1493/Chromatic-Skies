using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem32 : EnemyShooter<Laser>
{
    const int LaserCount = 7;
    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3f);

        for (int i = 0; i < LaserCount; i++)
        {
            float z = PlayerPosition.GetRotationDifference(transform.position);
            Vector3 pos = Vector3.zero;

            SpawnProjectile(0, z + 180f, pos).Fire();
            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}