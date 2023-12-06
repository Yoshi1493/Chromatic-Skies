using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 15;
    const int BranchCount = 5;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 5;
    const float BulletSpawnRadius = 2.5f;
    const float BulletBaseSpeed = 1.5f;
    const float BulletSpeedModifier = 0.2f;
    const float BulletRotationSpeed = -47.5f;
    const float BulletRotationSpeedModifier = 5f;
    const float BulletRotationDuration = 1f;

    List<EnemyBullet> bullets = new(WaveCount * BranchCount * BulletCount);

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        //StartMoveAction?.Invoke();
        //SetSubsystemEnabled(1);

        while (enabled)
        {
            bullets.Clear();

            for (int i = 0; i < WaveCount; i++)
            {
                float t = i / (WaveCount - 1f);
                bulletData.colour = bulletData.gradient.Evaluate(t);

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float r = ii * BranchSpacing;
                    Vector3 v1 = transform.up.RotateVectorBy(r);
                    Vector3 v2 = transform.up.RotateVectorBy(r + (BranchSpacing * 2f));
                    Vector3 pos = BulletSpawnRadius * Vector3.Lerp(v1, v2, (float)i / WaveCount);

                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = r + 180f - ((BranchCount - 2) * 180f / BranchCount / 4);        //r + 153
                        bullets.Add(SpawnProjectile(0, z, pos));
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(0.5f);

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        int b = (i * BranchCount * BulletCount) + (ii * BulletCount) + iii;
                        float r = transform.eulerAngles.z - (iii * BranchSpacing);
                        float s = BulletBaseSpeed + (i * BulletSpeedModifier);

                        bullets[b].StartCoroutine(bullets[b].RotateBy(r, 0f));
                        bullets[b].StartCoroutine(bullets[b].LerpSpeed(5f, 0f, BulletRotationDuration));

                        bullets[b].StartCoroutine(bullets[b].RotateBy(180f, 0.2f, delay: BulletRotationDuration));

                        r = BulletRotationSpeed + (i * BulletRotationSpeedModifier);
                        bullets[b].StartCoroutine(bullets[b].RotateBy(r, BulletRotationDuration, delay: BulletRotationDuration));
                        bullets[b].StartCoroutine(bullets[b].LerpSpeed(0f, s, 2f, delay: BulletRotationDuration));
                    }
                }
            }

            yield return WaitForSeconds(10f);
        }
    }
}