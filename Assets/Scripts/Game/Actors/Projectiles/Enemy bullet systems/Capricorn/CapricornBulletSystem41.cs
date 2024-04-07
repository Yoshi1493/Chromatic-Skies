using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class CapricornBulletSystem41 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 24;
    const float BranchSpacing = 20f;
    const int BulletCount = 12;
    const float BulletSpacing = 2f;
    const float BulletBaseSpeed = 3f;
    const float BulletSpeedModifier = 0.3f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < BranchCount; i++)
        {
            float z = RandomAngleDeg;

            for (int ii = 0; ii < BulletCount; ii++) 
            {
                float s = BulletBaseSpeed + (ii * BulletSpeedModifier);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(ii / (BulletCount - 1f));

                var bullet = SpawnProjectile(1, z, pos);
                bullet.MoveSpeed = s;
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}