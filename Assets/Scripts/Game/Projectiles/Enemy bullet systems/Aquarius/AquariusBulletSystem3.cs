using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 25;
    const float WaveSpacing = 8f;
    const int BranchCount = 16;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpacing = 0.8f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 1; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = (i * WaveSpacing) + (ii * BranchSpacing);
                    Vector3 pos = i * BulletSpacing * transform.up.RotateVectorBy(z);
                    bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                    SpawnProjectile(0, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return ownerShip.MoveToRandomPosition(2f, delay: 9f);
        }
    }
}