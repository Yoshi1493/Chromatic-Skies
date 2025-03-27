using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem2 : BossShooter<BossBullet>
{
    const float ArcWidth = 295f;
    const int RepeatCount = 4;
    const float RepeatSpacing = 360f / RepeatCount;
    const int WaveCount = 60;
    const float WaveSpacing = ArcWidth / (WaveCount - 1);
    const int BranchCount = 3;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpawnRadius = 0.8f;
    const float BulletBaseSpeed = 2.4f;
    const float BulletSpeedModifier = 0.06f;
    const float BulletRotationSpeed = 9f;
    const float BulletRotationSpeedModifier = 3f;
    const float BulletRotationDuration = 5f;

    List<BossBullet> bullets = new(WaveCount * BranchCount);

    protected override float ShootingCooldown => 0.02f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            SetSubsystemEnabled(1);

            for (int i = 0; i < RepeatCount; i++)
            {
                bullets.Clear();

                for (int ii = 0; ii < WaveCount; ii++)
                {
                    for (int iii = 0; iii < BranchCount; iii++)
                    {
                        float t = (i * RepeatSpacing) + (iii * BranchSpacing) + 0;
                        float z = (ii * WaveSpacing) + t + ((360f - ArcWidth) / 2);
                        Vector3 pos = (BulletSpawnRadius * -transform.up.RotateVectorBy(z)) - transform.up.RotateVectorBy(t);

                        bulletData.colour = bulletData.gradient.Evaluate(ii / (WaveCount - 1f));
                        bullets.Add(SpawnProjectile(0, z, pos));
                    }

                    yield return WaitForSeconds(ShootingCooldown);
                }

                yield return WaitForSeconds(0.5f);

                for (int ii = 0; ii < bullets.Count; ii++)
                {
                    float r = ii % (BulletRotationSpeed + (i * BulletRotationSpeedModifier)) * 2f;
                    float s = BulletBaseSpeed + (ii / BranchCount * BulletSpeedModifier);

                    if (bullets[ii].isActiveAndEnabled)
                    {
                        bullets[ii].StartCoroutine(bullets[ii].RotateBy(r, BulletRotationDuration));
                        bullets[ii].StartCoroutine(bullets[ii].LerpSpeed(0f, s, 2f));
                    }
                }
            }

            bullets.Clear();

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(1f);
        }
    }
}