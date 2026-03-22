using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiShooter08 : CommonEnemyShooter<EnemyBullet>
{
    const int BranchCount = 8;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 3;
    const float BulletSpacing = 3f;
    const float BulletBaseSpeed = 3.2f;
    const float BulletSpeedModifier = 0.4f;

    protected override float ShootingCooldown => 2.5f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(ShootingCooldown);

        float r = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < BranchCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (i * BranchSpacing) + ((ii - ((BulletCount - 1) / 2f)) * BulletSpacing) + r;
                float s = BulletBaseSpeed + (ii % 2 * BulletSpeedModifier);
                Vector3 pos = Vector3.zero;

                var bullet = SpawnProjectile(8, z, pos);
                bullet.MoveSpeed = s;
            }
        }
    }
}