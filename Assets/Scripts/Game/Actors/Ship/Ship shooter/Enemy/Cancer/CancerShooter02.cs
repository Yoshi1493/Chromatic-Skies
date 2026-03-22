using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerShooter02 : CommonEnemyShooter<EnemyBullet>
{
    const int BranchCount = 8;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 4;
    const float BulletRotationSpeed = 360f / BulletCount;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        float r = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < BranchCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (i * BranchSpacing) + r;
                Vector3 pos = Vector3.zero;

                var bullet = SpawnProjectile(2, z, pos);
                bullet.StartCoroutine(bullet.RotateBy(ii * BulletRotationSpeed, 0f, delay: 1.5f));
                bullet.Fire();
            }
        }
    }
}