using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class CancerBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const float ArcWidth = 295f;
    const int WaveCount = 4;
    const float WaveSpacing = 360f / WaveCount;
    const int BranchCount = 3;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 60;
    const float BulletSpacing = ArcWidth / (BulletCount - 1);
    const float BulletSpawnRadius = 0.8f;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.06f;

    List<EnemyBullet> bullets = new(BulletCount * BranchCount);

    protected override float ShootingCooldown => 0.02f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            SetSubsystemEnabled(1);

            float r = RandomAngleDeg;

            for (int i = 0; i < WaveCount; i++)
            {
                bullets.Clear();

                for (int ii = 0; ii < BulletCount; ii++)
                {
                    for (int iii = 0; iii < BranchCount; iii++)
                    {
                        float t = (i * WaveSpacing) + (iii * BranchSpacing) + 0;
                        float z = (ii * BulletSpacing) + t + ((360f - ArcWidth) / 2);
                        Vector3 pos = (BulletSpawnRadius * -transform.up.RotateVectorBy(z)) - transform.up.RotateVectorBy(t);

                        bulletData.colour = bulletData.gradient.Evaluate(ii / (BulletCount - 1f));
                        bullets.Add(SpawnProjectile(0, z, pos));
                    }

                    yield return WaitForSeconds(ShootingCooldown);
                }

                yield return WaitForSeconds(0.5f);

                for (int ii = 0; ii < bullets.Count; ii++)
                {
                    float s = BulletBaseSpeed + (ii / BranchCount * BulletSpeedModifier);
                    float t = ii % 12f * 2f;

                    bullets[ii].StartCoroutine(bullets[ii].LerpSpeed(0f, s, 2f));
                    bullets[ii].StartCoroutine(bullets[ii].RotateBy(t, 5f));
                }
            }

            bullets.Clear();
            yield return WaitForSeconds(1f);

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(2f);
        }
    }
}