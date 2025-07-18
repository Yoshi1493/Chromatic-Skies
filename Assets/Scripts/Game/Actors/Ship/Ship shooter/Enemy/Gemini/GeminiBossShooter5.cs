using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBossShooter5 : BossShooter<BossBullet>
{
    const float WaveSpacing = 30f;
    const int BranchCount = 2;
    const float BranchSpacing = (360f / BranchCount) - (WaveSpacing / 2f);
    const int BulletCount = 10;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpawnRadius = 0.5f;

    protected override float ShootingCooldown => 0.4f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();
        GeminiBossMovement5 movementSystem = (parentShip as Boss).GetCurrentMovementSystem() as GeminiBossMovement5;            //no

        SpawnProjectile(0, 0f, Vector3.zero).Fire();

        StartMoveAction?.Invoke();
        yield return WaitForSeconds(2f);

        for (int i = 0; enabled; i++)
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
    }
}