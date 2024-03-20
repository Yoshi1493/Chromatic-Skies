using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem42 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 1;

    protected override IEnumerator Shoot()
    {
        float r = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < BulletCount; i++)
        {
            float z = r;
            Vector3 pos = Vector3.zero;

            SpawnProjectile(2, z, Vector3.zero).Fire();
        }

        yield return WaitForSeconds(ShootingCooldown);
        enabled = false;
    }
}