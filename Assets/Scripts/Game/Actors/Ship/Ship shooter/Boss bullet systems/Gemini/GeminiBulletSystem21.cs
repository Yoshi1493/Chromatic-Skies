using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBulletSystem21 : BossShooter<BossBullet>
{
    const int WaveCount = 48;
    const float WaveSpacing = -10f;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpawnOffset = 0.25f;
    const int BulletCount = 3;
    const float BulletRotationSpeed = 4f;
    const float BulletRotationDuration = 1f;

    List<(Vector2 pos, float z)> bulletSpawnData = new(WaveCount * BranchCount);

    protected override float ShootingCooldown => 1f / 60;

    protected override IEnumerator Shoot()
    {
        bulletSpawnData.Clear();

        for (int i = 1; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                int d = ii % 2 * 2 - 1;
                float z = d * i * WaveSpacing;
                float x = d * i * BulletSpawnOffset;
                float y = 0f;
                Vector3 pos = new(x, y);

                SpawnProjectile(2, z, pos).Fire();
                bulletSpawnData.Add((pos, z));
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        yield return WaitForSeconds(2f);

        for (int i = 1; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                int b = BranchCount * (i - 1) + ii;
                var (pos, z) = bulletSpawnData[b];

                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float r = (iii - ((BulletCount - 1) / 2f)) * BulletRotationSpeed;

                    bulletData.colour = bulletData.gradient.Evaluate(iii / (BulletCount - 1f));

                    var bullet = SpawnProjectile(3, z, pos);
                    bullet.StartCoroutine(bullet.RotateBy(r, BulletRotationDuration, delay: 1f));
                    bullet.Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown * 3f);
        }

        enabled = false;
    }
}