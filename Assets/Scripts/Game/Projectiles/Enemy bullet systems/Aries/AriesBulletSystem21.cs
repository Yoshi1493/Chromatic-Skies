using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 6;
    const int BulletCount = 6;
    const float BulletBaseSpeed = 4f;
    const float BulletSpeedMultiplier = 0.4f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            float z = PlayerPosition.GetRotationDifference(transform.position);

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float s = BulletBaseSpeed + (ii * BulletSpeedMultiplier);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(ii / (BulletCount - 1f));

                var bullet = SpawnProjectile(1, z, pos);
                bullet.MoveSpeed = s;
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}