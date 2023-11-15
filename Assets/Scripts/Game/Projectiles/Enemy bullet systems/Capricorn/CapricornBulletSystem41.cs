using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class CapricornBulletSystem41 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 24;
    const float BranchSpacing = 20f;
    const int BulletCount = 12;
    const float BulletSpacing = 0.5f;
    const float BulletBaseSpeed = 3f;
    const float BulletSpeedModifier = 0.3f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < BranchCount; i++)
        {
            float z = RandomAngleDeg;

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float r = ii * BulletSpacing;
                float s = BulletBaseSpeed + (ii * BulletSpeedModifier);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(ii / (BulletCount - 1f));

                var bullet = SpawnProjectile(1, z, pos);
                bullet.MoveSpeed = s;
                bullet.Fire();

                bullet.StartCoroutine(bullet.RotateBy(r, 0.5f, delay: 2f));
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}