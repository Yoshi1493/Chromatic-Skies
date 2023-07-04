using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem41 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const float WaveSpacing = BulletSpacing / 2f;
    const int BulletCount = 16;
    const float BulletSpacing = 360f / BulletCount;

    protected override float ShootingCooldown => 0.5f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (i * WaveSpacing) + (ii * BulletSpacing);
                SpawnProjectile(1, z, Vector3.zero).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}