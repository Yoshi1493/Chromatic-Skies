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

    protected override float ShootingCooldown => 15f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        int r = 1;

        while (enabled)
        {
            StartMoveAction?.Invoke();

            for (int i = 0; i < BulletCount; i++)
            {
                float z = i * BulletSpacing;
                Vector3 pos = PlayerPosition + (BulletSpawnRadius * transform.up.RotateVectorBy(z));

                var bullet = SpawnProjectile(0, z, pos, false);
                bullet.StartCoroutine(bullet.RotateBy(r * BulletRotationSpeed, BulletRotationDuration));
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);

            r *= -1;
        }
    }
}