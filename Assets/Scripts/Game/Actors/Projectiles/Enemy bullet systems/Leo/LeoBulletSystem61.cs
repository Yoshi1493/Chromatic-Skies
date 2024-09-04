using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem61 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 36;
    const float WaveSpacing = 360f / WaveCount;
    const float MaxWaveAxialTilt = 30f;
    const int BranchCount = 9;
    const float BulletSpawnRadius = 1.5f;
    const float BulletSpawnRadiusModifier = 1.5f;

    List<EnemyBullet> bullets = new(WaveCount * BranchCount);

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                float r = Mathf.Min((i + 1) * BulletSpawnRadiusModifier, BulletSpawnRadius);
                float t = Mathf.Lerp(-MaxWaveAxialTilt, MaxWaveAxialTilt, i / (WaveCount - 1f));
                Vector3 v = r * Vector3.up.RotateVectorBy(t);

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = t + 90f;

                    float y = Mathf.Lerp(r, -r, ii / (BranchCount - 1f));
                    float x = Mathf.Sqrt((r * r) - (y * y));
                    Vector3 pos = r * new Vector3(x, y).RotateVectorBy(t);

                    var bullet = SpawnProjectile(1, z, pos) as LeoBullet61;
                    bullet.MoveSpeed = r;
                    bullet.rotationAxis = v;
                    bullets.Add(bullet);
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);

            for (int i = 0; i < BranchCount; i++)
            {
                for (int ii = 0; ii < WaveCount; ii++)
                {
                    int b = (ii * BranchCount) + i;

                    if (bullets[b].isActiveAndEnabled)
                    {
                        bullets[b].Fire();
                    }
                }

                yield return WaitForSeconds(0.5f);
            }

            yield return WaitForSeconds(10f);
        }
    }
}