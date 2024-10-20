using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem6 : EnemyShooter<EnemyBullet>
{
    const int RepeatCount = 3;
    const float RepeatSpacing = BranchSpacing / 2;
    const int WaveCount = 12;
    const float WaveSpacing = 360f / WaveCount;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 3;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpawnRadius = 1.2f;
    const float SpawnRadiusModifier = 0.6f;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.4f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);
        SetSubsystemEnabled(2);

        List<EnemyBullet> bullets = new(RepeatCount * WaveCount * BranchCount * BulletCount);

        while (enabled)
        {
            for (int i = 0; i < RepeatCount; i++)
            {
                for (int ii = 0; ii < WaveCount; ii++)
                {
                    float t = ii / (WaveCount - 1f);

                    for (int iii = 0; iii < BranchCount; iii++)
                    {
                        Vector3 v1 = transform.up.RotateVectorBy((i * RepeatSpacing) + (iii * BranchSpacing));
                        Vector3 v2 = transform.up.RotateVectorBy((i * RepeatSpacing) + ((iii + 1) * BranchSpacing));

                        for (int iv = 0; iv < BulletCount; iv++)
                        {
                            float z = iv * BulletSpacing;
                            Vector3 pos = (BulletSpawnRadius + (i * SpawnRadiusModifier)) * Vector3.Lerp(v1, v2, t);

                            bulletData.colour = bulletData.gradient.Evaluate(i / (RepeatCount - 1f));
                            bullets.Add(SpawnProjectile(0, z, pos));
                        }
                    }

                    yield return WaitForSeconds(ShootingCooldown);
                }
            }

            for (int i = RepeatCount - 1; i >= 0; i--)
            {
                for (int ii = 0; ii < WaveCount * BranchCount * BulletCount; ii++)
                {
                    int b = (i * WaveCount * BranchCount * BulletCount) + ii;
                    float s = BulletBaseSpeed - (i * BulletSpeedModifier);

                    bullets[b].MoveSpeed = s;
                    bullets[b].StartCoroutine(bullets[b].RotateBy(Random.Range(-BranchSpacing, BranchSpacing) * 2f, 3f));
                    bullets[b].Fire();
                }

                yield return WaitForSeconds(ShootingCooldown * 12f);
            }

            bullets.Clear();
            yield return WaitForSeconds(2f);

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(2f);
        }
    }
}