using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 4;
    const int BulletCount = 18;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedMultiplier = 0.2f;

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

                var bullet = SpawnProjectile(1, z, pos);
                bullet.MoveSpeed = s;
                bullet.Fire();
            } 

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }

}