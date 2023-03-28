using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem11 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const float WaveSpacing = BulletSpacing / WaveCount;
    const int BulletCount = 24;
    const int BulletSpacing = 360 / BulletCount;
    const float BulletBaseSpeed = 1f;
    const float BulletSpeedMultiplier = 2f;

    protected override float ShootingCooldown => 0.5f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float r = Random.value;

                float z = (i * WaveSpacing) + (ii * BulletSpacing);
                float s = BulletBaseSpeed + (r * BulletSpeedMultiplier);

                bulletData.colour = bulletData.gradient.Evaluate(r);
                var bullet = SpawnProjectile(1, z, Vector3.zero);
                bullet.MoveSpeed = s;
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}