using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusShooter06 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const int BranchCount = 20;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletRotationSpeed = 90f;
    const float BulletRotationDuration = 9f;

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return WaitForSeconds(3f);

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = ii * BranchSpacing;

                        bulletData.colour = bulletData.gradient.Evaluate(iii);
                        Vector3 pos = Vector3.zero;

                        var bullet = SpawnProjectile(6, z, pos);
                        bullet.StartCoroutine(bullet.RotateBy((iii % 2 * 2 - 1) * BulletRotationSpeed, BulletRotationDuration));
                        bullet.MoveSpeed = 2f;
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}