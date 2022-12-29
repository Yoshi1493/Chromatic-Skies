using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 16;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 6;
    const float BulletSpacing = 10f;

    protected override float ShootingCooldown => 0.12f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                float n = ((i % 2) - 0.5f) * 2;
                float r = BulletSpacing * n;

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = (r * (ii - 1)) + (iii * BranchSpacing) + (2.5f * n);
                        Vector3 pos = transform.up.RotateVectorBy(z);

                        SpawnProjectile(ii % 2, z, pos).Fire();
                    }

                    yield return WaitForSeconds(ShootingCooldown * 0.5f);
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            AttackFinishAction?.Invoke();
            yield return WaitForSeconds(3f);
        }
    }
}