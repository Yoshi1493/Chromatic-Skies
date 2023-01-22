using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class AriesBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 90;
    const float WaveSpacing = 4f;
    const int BranchCount = 5;
    const float BranchSpacing = 360 / BranchCount;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        StartMoveAction?.Invoke();
        SetSubsystemEnabled(1);

        while (enabled)
        {
            int d = PositiveOrNegativeOne;
            float n = 0f;
            float r = Random.Range(0f, 90f);

            for (int i = 1; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = ii * BranchSpacing + n + r;
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(0, z, pos).Fire();
                }

                n += i * WaveSpacing * d;
                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}