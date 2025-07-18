using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBossShooter3 : BossShooter<BossBullet>
{
    const int RepeatCount = 3;
    const int WaveCount = 6;
    const float WaveSpacing = 6f;
    const int BranchCount = 3;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 3;
    const float BulletSpacing = 15f;
    const float BulletSpawnRadius = 1f;
    const float SpawnRadiusModifier = -0.2f;

    protected override float ShootingCooldown => 0.15f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);
        yield return WaitForSeconds(1f);

        while (enabled)
        {
            for (int i = 0; i < RepeatCount; i++)
            {
                StartMoveAction?.Invoke();
                float r = Random.Range(0f, BranchSpacing);

                for (int ii = 0; ii < WaveCount; ii++)
                {
                    for (int iii = 0; iii < BranchCount; iii++)
                    {
                        for (int iv = 0; iv < BulletCount; iv++)
                        {
                            float z = -((ii * WaveSpacing) + (iii * BranchSpacing) + ((iv - ((BulletCount - 1) / 2f)) * BulletSpacing) + r);
                            Vector3 pos = (BulletSpawnRadius + (ii * SpawnRadiusModifier)) * transform.up.RotateVectorBy(z);

                            SpawnProjectile(0, z, pos).Fire();
                        }
                    }

                    yield return WaitForSeconds(ShootingCooldown);
                }

                yield return WaitForSeconds(1f);
            }

            SetSubsystemEnabled(2);
            yield return WaitForSeconds(3f);
        }

    }
}