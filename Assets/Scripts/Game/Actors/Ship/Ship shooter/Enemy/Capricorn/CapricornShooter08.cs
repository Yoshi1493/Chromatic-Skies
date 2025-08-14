using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornShooter08 : CommonEnemyShooter<EnemyBullet>
{
    const int BulletCount = 3;

    protected override float ShootingCooldown => 0.6f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < BulletCount; i++)
        {
            yield return WaitForSeconds(ShootingCooldown);

            float z = PlayerPosition.GetRotationDifference(transform.position);
            Vector3 pos = Vector3.zero;

            SpawnProjectile(8, z, pos).Fire();
        }
    }
}