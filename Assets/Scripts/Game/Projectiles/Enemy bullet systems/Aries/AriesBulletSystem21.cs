using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 6;
    const float BulletBaseSpeed = 4f;
    const float BulletSpeedMultiplier = 0.4f;

    protected override float ShootingCooldown => 5f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return WaitForSeconds(ShootingCooldown);

            float z = PlayerPosition.GetRotationDifference(transform.position);

            for (int i = 0; i < BulletCount; i++)
            {
                float s = BulletBaseSpeed + (i * BulletSpeedMultiplier);
                Vector3 pos = Vector3.zero;

                var bullet = SpawnProjectile(1, z, pos);
                bullet.MoveSpeed = s;
                bullet.Fire();
            }

            yield return WaitForSeconds(1f);
        }
    }

}