using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 20;
    const float WaveSpacing = 360f / WaveCount;
    const int BulletCount = 6;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.1f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            float r = i * WaveSpacing;
            bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = r + (ii * BulletSpacing);
                float s = BulletBaseSpeed + (i * BulletSpeedModifier);
                Vector3 pos = transform.up.RotateVectorBy(r);

                var bullet = SpawnProjectile(1, z, pos);
                bullet.MoveSpeed = s;
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}