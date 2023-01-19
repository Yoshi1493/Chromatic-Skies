using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem11 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const float WaveSpacing = BulletSpacing / 2f;
    const int BulletCount = 12;
    const float BulletSpacing = 360f / BulletCount;

    protected override float ShootingCooldown => 0.5f;

    protected override IEnumerator Shoot()
    {
        float r = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (i * WaveSpacing) + (ii * BulletSpacing) + r;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(2, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}