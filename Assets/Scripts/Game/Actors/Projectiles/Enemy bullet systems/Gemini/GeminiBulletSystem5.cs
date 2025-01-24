using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 24;
    const float WaveSpacing = 720f / WaveCount;
    const int BranchCount = 2;
    const float BranchSpacing = (360f / BranchCount) - (WaveSpacing / 2f);
    const int BulletCount = 10;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpawnRadius = 0.5f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();
        GeminiMovementSystem5 movementSystem = (ownerShip as Enemy).GetCurrentMovementSystem() as GeminiMovementSystem5;            //no

        SpawnProjectile(0, 0f, Vector3.zero).Fire();

        StartMoveAction?.Invoke();
        yield return WaitForSeconds(2f);

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = (i * WaveSpacing) + ((ii + 0.5f) * BranchSpacing);
                        float r = iii * BulletSpacing;
                        Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(r);

                        bulletData.colour = bulletData.gradient.Evaluate(ii);

                        SpawnProjectile(2, z, pos).Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(3f);

            movementSystem.Teleport();
            yield return WaitForSeconds(2f);
        }

    }
}