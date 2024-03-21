using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 32;
    const float BulletSpacing = 720f / BulletCount;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        float r = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < BulletCount; i++)
        {
            float z = (i * BulletSpacing) + r;
            Vector3 pos = Vector3.zero;

            SpawnProjectile(2, z, pos).Fire();
            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}