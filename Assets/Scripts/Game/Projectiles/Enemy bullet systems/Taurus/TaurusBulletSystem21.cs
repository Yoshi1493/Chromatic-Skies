using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 16;
    const float BulletSpacing = 360f / BulletCount;
    const int WaveCount = 3;
    const float WaveSpacing = BulletSpacing / 2f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return WaitForSeconds(2f);

            float theta = PlayerPosition.GetRotationDifference(transform.position);

            for (int i = 0; i < WaveCount; i++)
            {
                for (int j = 0; j < BulletCount; j++)
                {
                    float z = (i * WaveSpacing) + (j * BulletSpacing) + theta;
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(1, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}