using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class LeoBulletSystem6 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 24;
    const float WaveSpacing = 360f / WaveCount;
    const int BranchCount = 15;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 0.25f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            float r = RandomAngleDeg;

            for (int i = 0; i < WaveCount; i++)
            {
                for (int j = 0; j < BranchCount; j++)
                {
                    float z = (i * WaveSpacing) + (j * BranchSpacing);
                    Vector3 pos = transform.up.RotateVectorBy(-z + r);

                    SpawnProjectile(0, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            //yield return ownerShip.MoveToRandomPosition(1f);
        }
    }
}