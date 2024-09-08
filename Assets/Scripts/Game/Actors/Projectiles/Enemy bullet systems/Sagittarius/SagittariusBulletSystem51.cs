using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBulletSystem51 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 24;
    const float BulletSpacing = 180f / BulletCount;
    const float BulletRotationSpeed = 60f;
    const float BulletRotationDuration = 8f;

    protected override float ShootingCooldown => 5f;

    protected override IEnumerator Shoot()
    {
        for (int i = 1; enabled; i *= -1)
        {
            yield return WaitForSeconds(ShootingCooldown);

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (ii * BulletSpacing) - 90f;
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(ii / (BulletCount - 1f));

                var bullet = SpawnProjectile(2, z, pos);
                bullet.StartCoroutine(bullet.RotateBy(i * BulletRotationSpeed, BulletRotationDuration));
                bullet.Fire();
            }
        }
    }
}