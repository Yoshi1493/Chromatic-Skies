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

        StartMoveAction?.Invoke();
        SetSubsystemEnabled(1);
        SetSubsystemEnabled(2);
        SetSubsystemEnabled(3);

        for (int i = 0; i < RingCount; i++)
        {
            float d = (i % 2 * 2 - 1) * 90f;

            for (int ii = 0; ii < BranchCount; ii++)
            {
                float r = ii * BranchSpacing;

                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = r + ((iii - ((BulletCount - 1) / 2f)) * BulletSpacing) + d;
                    Vector3 pos = (BulletSpawnRadius + (i * SpawnRadiusModifier))* transform.up.RotateVectorBy(z + d);

                    bulletData.colour = bulletData.gradient.Evaluate(i % 2);
                    bullets.Add(SpawnProjectile(0, z, pos));
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        yield return WaitForSeconds(0.5f);

        for (int i = 0; i < RingCount; i++)
        {
            int r = i % 2 * 2 - 1;

            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    int b = (i * BranchCount * BulletCount) + (ii * BulletCount) + iii;                 

                    bullets[b].StartCoroutine(bullets[b].TransformRotateAround(EnemyMovementBehaviour.originalPosition, Mathf.Infinity, r * BulletRotationSpeed));
                }
            }
        }

    }
}