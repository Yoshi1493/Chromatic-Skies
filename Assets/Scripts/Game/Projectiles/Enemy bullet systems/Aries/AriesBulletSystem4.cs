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

        SetSubsystemEnabled(1);

        while (enabled)
        {
            yield return WaitForSeconds(2f);

            int d = PositiveOrNegativeOne;
            float n = 0f;
            float o = Random.Range(0f, 90f);

            for (int i = 1; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = ii * BranchSpacing + n + o;
                    SpawnProjectile(0, z, Vector2.zero).Fire();
                }

                n += i * WaveSpacing * d;
                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return ownerShip.MoveToRandomPosition(1f, delay: 1f);
        }
    }
}