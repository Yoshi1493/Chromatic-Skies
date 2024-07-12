using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int RingCount = 12;
    const int BranchCount = 5;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 5;
    const float BulletSpacing = 10f;
    const float BulletSpawnRadius = 2f;
    const float SpawnRadiusModifier = 1f;
    const float BulletRotationSpeed = 12f;

    List<EnemyBullet> bullets = new(RingCount * BranchCount * BulletCount);

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        //StartMoveAction?.Invoke();

        for (int i = 0; i < RingCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float r = ii * BranchSpacing;

                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = r + ((iii - ((BulletCount - 1) / 2f)) * BulletSpacing) + 180f;
                    Vector3 pos = (BulletSpawnRadius + (i * SpawnRadiusModifier))* transform.up.RotateVectorBy(z);

                    bulletData.colour = bulletData.gradient.Evaluate(i % 2);
                    bullets.Add(SpawnProjectile(0, z, pos));
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        for (int i = 0; i < RingCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    int b = (i * BranchCount * BulletCount) + (ii * BulletCount) + iii;
                    int r = i % 2 * 2 - 1;

                    bullets[b].StartCoroutine(bullets[b].TransformRotateAround(EnemyMovementBehaviour.originalPosition, Mathf.Infinity, r * BulletRotationSpeed));
                }
            }
        }
    }
}