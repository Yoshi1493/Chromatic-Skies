using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem41 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 18;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletStartSpeed = 4f;
    const float BulletEndSpeed = 1.5f;
    const float BulletRotationAmount = 90f;
    const float BulletRotationDuration = 5f;

    protected override float ShootingCooldown => 1.5f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3f);

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
                    bullet.StartCoroutine(bullet.LerpSpeed(BulletStartSpeed, BulletEndSpeed, 2f));
                    bullet.StartCoroutine(bullet.RotateBy((ii % 2 * 2 - 1) * BulletRotationAmount, BulletRotationDuration));
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}