using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem6 : EnemyShooter<EnemyBullet>
{
    const float ArcHalfWidth = 30f;
    const int BulletCount = 12;
    const float BulletSpawnRadius = 0.1f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        List<float> bulletSpawnData = new(BulletCount);

        StartMoveAction?.Invoke();
        SetSubsystemEnabled(1);

        while (enabled)
        {
            for (int i = 0; i < BulletCount; i++)
            {
                bulletSpawnData.Add((i % 2 * 2 - 1) * Random.Range(0f, ArcHalfWidth) + 180f);
            }
            bulletSpawnData.Randomize();

            for (int i = 0; i < BulletCount; i++)
            {
                float z = bulletSpawnData[i];
                Vector3 pos = BulletSpawnRadius * Random.insideUnitCircle;

                SpawnProjectile(0, z, pos).Fire();
                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(5f);
            bulletSpawnData.Clear();
        }
    }
}