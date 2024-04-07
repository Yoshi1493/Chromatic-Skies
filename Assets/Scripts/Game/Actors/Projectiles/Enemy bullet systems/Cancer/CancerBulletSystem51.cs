using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem51 : EnemyShooter<EnemyBullet>
{
    protected override float ShootingCooldown => 1.5f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return WaitForSeconds(ShootingCooldown);

            float z = PlayerPosition.GetRotationDifference(transform.position);
            Vector3 pos = Vector3.zero;

            SpawnProjectile(2, z, pos).Fire();
        }
    }
}