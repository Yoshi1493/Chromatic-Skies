using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 6;
    const int BranchSpacing = 360 / BranchCount;
    const int BulletCount = 6;
    const float BulletSpacing = 1.2f;

    protected override float ShootingCooldown => 0.5f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            SetSubsystemEnabled(1);
            float randStartAngle = Random.Range(0f, BranchSpacing);

            for (int i = 1; i <= BranchCount; i++)
            {
                float xOffset = (i - 1) * BulletSpacing * 0.5f;

                for (int j = 0; j < i; j++)
                {
                    Vector3 offset = (j * BulletSpacing - xOffset) * Vector3.right;

                    for (int k = 0; k < BulletCount; k++)
                    {
                        float z = k * BranchSpacing + randStartAngle;
                        SpawnProjectile(0, z, offset.RotateVectorBy(z)).Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return ownerShip.MoveToRandomPosition(1f, delay: 2f);
            yield return WaitForSeconds(4f);
        }
    }
}