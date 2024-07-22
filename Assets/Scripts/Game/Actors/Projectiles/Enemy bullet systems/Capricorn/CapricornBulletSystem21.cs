using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class CapricornBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 12;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 8;
    const float BulletSpacing = 12f;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.2f;

    protected override float ShootingCooldown => 5 / 60f;

    protected override IEnumerator Shoot()
    {
        float r = RandomAngleDeg;

        for (int i = 0; i <= BranchCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (i * BranchSpacing) - (ii * BulletSpacing) + r;
                float s = BulletBaseSpeed + (ii * BulletSpeedModifier);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(ii / (BulletCount - 1f));

                var bullet = SpawnProjectile(2, z, pos);
                bullet.MoveSpeed = s;
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}