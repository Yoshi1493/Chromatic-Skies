using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem6 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const float WaveSpacing = 180f;
    const int BranchCount = 5;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 36;
    const float MaxBulletSpacing = 360f * (BranchCount - 1) / BranchCount / 2;
    const float RingRadiusMultiplier = 0.4f;
    const int BulletClumpCount = 3;
    const float BulletClumpSpacing = MaxBulletSpacing * 2f / BulletCount;

    List<EnemyBullet> bullets = new(BranchCount * BulletCount);

    protected override float ShootingCooldown => 0.025f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    for (int iii = 0; iii < BranchCount; iii++)
                    {
                        float r = (i * WaveSpacing) + (iii * BranchSpacing);
                        float z = Mathf.Lerp(-MaxBulletSpacing, MaxBulletSpacing, (float)ii / BulletCount);

                        Vector3 v1 = (i * RingRadiusMultiplier + 1) * transform.up.RotateVectorBy(r);
                        Vector3 v2 = v1 + v1.RotateVectorBy(z);

                        bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));
                        var bullet = SpawnProjectile(0, z + r, v2);
                        bullets.Add(bullet);
                    }

                    yield return WaitForSeconds(ShootingCooldown);
                }
            }

            StartCoroutine(Fire());
            yield return WaitForSeconds(1f);

            SetSubsystemEnabled(1);
            yield return WaitForSeconds(1f);

            StartMoveAction?.Invoke();
            yield return WaitUntil(() => bullets.Count == 0);
        }
    }

    IEnumerator Fire()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    int b = (i * BranchCount * BulletCount) + (ii * BranchCount) + iii;

                    float z = (b / BranchCount % BulletClumpCount - ((BulletClumpCount - 1) / 2f)) * BulletClumpSpacing;
                    bullets[b].StartCoroutine(bullets[b].RotateBy(z, 2f));
                    bullets[b].Fire();
                }

                yield return WaitForSeconds(ShootingCooldown * 2f);
            }
        }

        bullets.Clear();
    }
}