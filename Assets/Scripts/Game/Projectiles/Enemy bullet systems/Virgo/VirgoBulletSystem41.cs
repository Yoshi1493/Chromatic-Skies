using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem41 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 18;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletStartSpeed = 4f;
    const float BulletEndSpeed = 2.5f;
    const float BulletRotationAmount = 120f;
    const float BulletRotationDuration = 5f;

    protected override float ShootingCooldown => 2.0f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(ShootingCooldown);

        while (enabled)
        {
            for (int i = 0; i < BranchCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float z = i * BranchSpacing;
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(ii);

                    var bullet = SpawnProjectile(1, z, pos);
                    bullet.StartCoroutine(bullet.LerpSpeed(BulletStartSpeed, BulletEndSpeed, 1f));
                    bullet.StartCoroutine(bullet.RotateBy((ii % 2 * 2 - 1) * BulletRotationAmount, BulletRotationDuration));
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}