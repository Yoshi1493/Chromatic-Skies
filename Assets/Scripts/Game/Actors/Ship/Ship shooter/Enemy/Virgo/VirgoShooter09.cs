using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoShooter09 : CommonEnemyShooter<EnemyBullet>
{
    const int BranchCount = 5;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletCount = 5;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpawnRadius = 0.4f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        for (int i = 0; i < BranchCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = PlayerPosition.GetRotationDifference(transform.position);
                float t = ((i - ((BranchCount - 1) / 2f)) * BranchSpacing) + z;
                Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(t);

                bulletData.colour = bulletData.gradient.Evaluate(i / (BranchCount - 1f));

                var bullet = SpawnProjectile(9, z, pos);
                bullet.StartCoroutine(bullet.RotateBy((z - t) + (ii * BulletSpacing), 0f, delay: 1f));
                bullet.Fire();
            }
        }
    }
}