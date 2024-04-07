using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem61 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 36;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletRotationSpeed = 90f;

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3f);

        for (int i = 1; enabled; i *= -1)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = ii * BulletSpacing;
                Vector3 pos = Vector3.zero;

                var bullet = SpawnProjectile(2, z, pos);
                bullet.StartCoroutine(bullet.RotateBy(i * BulletRotationSpeed, 5f));
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}