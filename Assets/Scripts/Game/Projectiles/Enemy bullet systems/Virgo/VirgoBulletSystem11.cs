using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem11 : EnemyBulletSubsystem<EnemyBullet>
{
    const int WaveCount = 55;
    const float WaveSpacing = 10f;
    const int BranchCount = 5;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return WaitForSeconds(1f);

            float r = Random.Range(0f, BranchSpacing);

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = -((i * WaveSpacing) + (ii * BranchSpacing)) + r;
                    SpawnProjectile(1, z, Vector3.zero).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return ownerShip.MoveToRandomPosition(1f, delay: 2f);
        }
    }
}