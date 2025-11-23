using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusShooter07 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const int BranchCount = 12;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletRotationSpeed = 90f;
    const float BulletRotationDuration = 6f;

    protected override float ShootingCooldown => 1.0f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.7f);

        while (enabled)
        {
            yield return WaitForSeconds(6.6f);

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = (iii % 2 * 2 - 1) * (ii * BranchSpacing);
                        Vector3 pos = Vector3.zero;

                        bulletData.colour = bulletData.gradient.Evaluate(iii);

                        var bullet = SpawnProjectile(5, z, pos);
                        bullet.StartCoroutine(bullet.RotateBy((iii % 2 * 2 - 1) * BulletRotationSpeed, BulletRotationDuration));
                        bullet.Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(2f);
        }
    }
}