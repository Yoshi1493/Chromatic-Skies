using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 100;
    const float WaveSpacing = 5f;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpawnRadius = 1f;
    const float SpawnRadiusModifier = 0.01f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();
        GeminiMovementSystem5 movementSystem = (ownerShip as Enemy).GetCurrentMovementSystem() as GeminiMovementSystem5;            //no

        SpawnProjectile(0, 0f, Vector3.zero).Fire();

        StartMoveAction?.Invoke();
        yield return WaitForSeconds(2f);

        while (enabled)
        {
            float i = 0;

            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    float z = (ii * WaveSpacing) + (iii * BranchSpacing) + i;
                    Vector3 pos = Mathf.PingPong(i * SpawnRadiusModifier, BulletSpawnRadius) * transform.up.RotateVectorBy(z);

                    SpawnProjectile(2, z, pos).Fire();
                }

                i = (i + (ii * 0.1f)) % 360f;
                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);
            movementSystem.Teleport();
            yield return WaitForSeconds(1f);
        }

    }
}