using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 150;
    const float WaveSpacing = 10f;
    const int BulletCount = 3;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletRotationSpeed = 10f;
    const float BulletRotationDuration = 2f;
    const float BulletSpawnOffset = 0.2f;
    const float BulletSpawnRadius = 0.1f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            SetSubsystemEnabled(1);
            SetSubsystemEnabled(2);

            float r = Random.Range(0, 4) * 90f;

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float z = (i * WaveSpacing) + r;
                    float t = (ii - 1) * BulletSpacing;
                    Vector3 v = (ii - ((BulletCount - 1) / 2f)) * BulletSpawnOffset * Vector3.right;
                    Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(z + t) + v.RotateVectorBy(z);

                    SpawnProjectile(0, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(2f);
        }
    }
}