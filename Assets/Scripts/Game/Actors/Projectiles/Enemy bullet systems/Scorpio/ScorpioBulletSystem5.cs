using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 12;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpawnRadius = 0.5f;
    const float BulletRotationSpeed = 60f;
    const float BulletRotationDuration = 3f;

    protected override float ShootingCooldown => 12f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        for (int i = 1; enabled; i *= -1)
        {
            StartMoveAction?.Invoke();
            SetSubsystemEnabled(1);

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = ii * BulletSpacing;
                Vector3 pos = PlayerPosition + (BulletSpawnRadius * transform.up.RotateVectorBy(z));

                var bullet = SpawnProjectile(0, z, pos, false);
                bullet.StartCoroutine(bullet.RotateBy(i * BulletRotationSpeed, BulletRotationDuration));
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}