using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class LeoBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 99;
    const float WaveSpacing = 15f;
    const int BranchCount = 4;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            StartMoveAction?.Invoke();
            SetSubsystemEnabled(1);

            yield return WaitForSeconds(4f);

            float r = Random.Range(0f, BranchSpacing);
            int d = PositiveOrNegativeOne;

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    int b = Random.Range(0, enemyProjectiles.Count);
                    float z = d * ((i * WaveSpacing) + (ii * BranchSpacing) + r);
                    Vector3 pos = Vector3.up.RotateVectorBy(z * 0.5f);

                    SpawnProjectile(b, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}