using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem62 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 20;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletRotationSpeed = 45f;
    const float BulletRotationDuration = 5f;

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(ShootingCooldown);

        for (int i = 0; enabled; i++)
        {
            yield return WaitForSeconds(ShootingCooldown);

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = ii * BulletSpacing;
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(ii / (BulletCount - 1f));

                var bullet = SpawnProjectile(2, z, pos);
                bullet.StartCoroutine(bullet.RotateBy((i % 2 * 2 - 1) * BulletRotationSpeed, BulletRotationDuration));
                bullet.Fire();
            }
        }
    }
}