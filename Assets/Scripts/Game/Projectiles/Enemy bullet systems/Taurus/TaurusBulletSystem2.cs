using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 24;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletSpacing = 360f / BulletCount;

    protected override float ShootingCooldown => 0.12f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);
        StartCoroutine(Move());

        while (enabled)
        {
            for (int i = 0; i < BranchCount; i++)
            {
                for (int j = 0; j < BulletCount; j++)
                {
                    float z = i * BranchSpacing;
                    float r = j * BulletSpacing;
                    Vector3 pos = transform.up.RotateVectorBy(z + r);

                    SpawnProjectile(0, z + 90f, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }

    }

    IEnumerator Move()
    {
        while (enabled)
        {
            yield return WaitForSeconds(2.5f);
            //yield return ownerShip.MoveToRandomPosition(1f, 1f, 4f);
        }
    }
}