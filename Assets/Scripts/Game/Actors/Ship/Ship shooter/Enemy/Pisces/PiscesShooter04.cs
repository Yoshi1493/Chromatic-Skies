using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesShooter04 : CommonEnemyShooter<EnemyBullet>
{
    const int BranchCount = 12;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletRotationSpeed = 90f;
    const float BulletRotationDuration = 3f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        for (int i = 0; i < BranchCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = i * BranchSpacing;
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(ii);

                var bullet = SpawnProjectile(4, z, pos);
                bullet.StartCoroutine(bullet.RotateBy((ii % 2 * 2 - 1) * BulletRotationSpeed, BulletRotationDuration, delay: 0.5f));
                bullet.Fire();
            }
        }
    }
}