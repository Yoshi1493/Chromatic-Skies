using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesShooter03 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 6;
    const int BranchCount = 6;
    const float BranchSpacing = 6f;
    const int BulletCount = 6;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletRotationSpeed = 30f;
    const float BulletRotationSpeedModifier = -6f;
    const float BulletRotationDuration = 3f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                int d = i % 2 * 2 - 1;

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = d * ((ii * BranchSpacing) + (iii * BulletSpacing));
                        Vector3 pos = Vector3.zero;

                        bulletData.colour = bulletData.gradient.Evaluate(i % 2);

                        var bullet = SpawnProjectile(3, z, pos);
                        bullet.StartCoroutine(bullet.RotateBy(d * (BulletRotationSpeed + (ii * BulletRotationSpeedModifier)), BulletRotationDuration));
                        bullet.Fire();
                    }

                    yield return WaitForSeconds(ShootingCooldown);
                }

                yield return WaitForSeconds(ShootingCooldown * 2f);
            }

            yield return WaitForSeconds(1f);
        }
    }
}