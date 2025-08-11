using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornShooter03 : CommonEnemyShooter<EnemyBullet>
{
    const int BulletCount = 6;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpawnRadius = 0.5f;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.2f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        float z = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < BulletCount; i++)
        {
            float r = ((i + 0.5f) * BulletSpacing) + z;
            float s = BulletBaseSpeed + (i * BulletSpeedModifier);
            Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(r);

            bulletData.colour = bulletData.gradient.Evaluate(i / (BulletCount - 1f));

            var bullet = SpawnProjectile(0, z, pos);
            bullet.MoveSpeed = s;
            bullet.Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}