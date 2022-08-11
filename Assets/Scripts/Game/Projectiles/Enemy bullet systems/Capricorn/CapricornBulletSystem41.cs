using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem41 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 13;
    const float BranchSpacing = 20f;
    const int BulletCount = 6;
    const float BulletSpeed = 3f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(.2f);

        for (int i = 0; i < BranchCount; i++)
        {
            float r = PlayerPosition.GetRotationDifference(transform.position);
            float z = ((i - BranchCount / 2) * BranchSpacing) + r;
            Vector3 pos = Vector3.zero;

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float s = BulletSpeed + (ii * 0.4f);

                var bullet = SpawnProjectile(1, z, pos);
                bullet.MoveSpeed = s;
                bullet.Fire();

                bullet = SpawnProjectile(2, z, pos);
                bullet.MoveSpeed = s;
                bullet.Fire();
            }
        }

        enabled = false;
    }
}