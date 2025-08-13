using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornShooter06 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 2;
    const int BranchCount = 48;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletBaseSpeed = 2.5f;
    const float BulletSpeedModifier = 0.5f;
    const float BulletRotationSpeed = 90f;
    const float BulletRotationDuration = 3f;

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = ii * BranchSpacing;
                    float s = BulletBaseSpeed + (i * BulletSpeedModifier);
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(i);

                    var bullet = SpawnProjectile(6, z, pos);
                    if (i == 0)
                    {
                        bullet.StartCoroutine(bullet.RotateBy((ii % 2 * 2 - 1) * BulletRotationSpeed, BulletRotationDuration, delay: 0.5f));
                    }
                    bullet.MoveSpeed = s;
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(3f);
        }

    }
}