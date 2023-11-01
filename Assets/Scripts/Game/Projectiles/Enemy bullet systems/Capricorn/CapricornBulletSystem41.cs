using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class CapricornBulletSystem41 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 24;
    const float BranchSpacing = 20f;
    const int BulletCount = 12;
    const float BulletBaseSpeed = 3f;
    const float BulletSpeedModifier = 0.4f;
    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < BranchCount; i++)
        {
            float z = RandomAngleDeg;
            Vector3 pos = Vector3.zero;

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float s = BulletBaseSpeed + (ii * BulletSpeedModifier);
                bulletData.colour = bulletData.gradient.Evaluate(ii / (BulletCount - 1f));

                var bullet = SpawnProjectile(1, z, pos);
                bullet.MoveSpeed = s;
                bullet.Fire();

                bullet.StartCoroutine(bullet.LerpSpeed(BulletBaseSpeed, 0f, 1f));
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}