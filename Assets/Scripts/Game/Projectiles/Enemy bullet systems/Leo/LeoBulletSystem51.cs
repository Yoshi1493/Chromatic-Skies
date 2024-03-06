using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem51 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 20;
    const float WaveSpacing = 720f / WaveCount;
    const int BulletClumpCount = 5;
    const float BulletSpacing = 5f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletClumpCount; ii++)
            {
                float z = (i * WaveSpacing) + (ii * BulletSpacing);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                SpawnProjectile(4, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}