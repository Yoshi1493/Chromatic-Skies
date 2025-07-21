using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusShooter06 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 8;
    const float WaveSpacing = 360f / WaveCount;
    const int BulletCount = 3;
    const float BulletBaseSpeed = 2.5f;
    const float BulletSpeedModifier = 0.5f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        float r = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (i * WaveSpacing) + r;
                float s = BulletBaseSpeed + (ii * BulletSpeedModifier);

                Vector3 pos = Vector3.zero;

                var bullet = SpawnProjectile(7, z, pos);
                bullet.MoveSpeed = s;
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

    }
}