using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 45;
    const float WaveSpacing = 5f;
    const int BranchCount = 4;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 0.025f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = (i * WaveSpacing) + (ii * BranchSpacing);
                    Vector3 pos = Vector3.Lerp(Vector3.left, Vector3.right, i / (float)WaveCount);

                    SpawnProjectile(i % 2, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return ownerShip.MoveToRandomPosition(2f, 1f, 1.44f, 1f);
        }
    }
}