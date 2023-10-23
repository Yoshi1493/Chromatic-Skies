using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem11 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 12;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletRotationSpeed = 60f;

    protected override float ShootingCooldown => 3.0f;

    protected override IEnumerator Shoot()
    {
        for (int i = 1; enabled; i *= -1)
        {
            yield return WaitForSeconds(ShootingCooldown);

            float r = PlayerPosition.GetRotationDifference(transform.position);

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (ii * BulletSpacing) + r;
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(i % 2);

                var bullet = SpawnProjectile(1, z, pos);
                bullet.StartCoroutine(bullet.RotateBy(i * BulletRotationSpeed, 5f));
                bullet.Fire();
            }
        }
    }
}