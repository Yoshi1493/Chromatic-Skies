using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornShooter06 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 2;
    const int BranchCount = 45;
    const float BranchSpacing = 5f;
    const int BulletCount = 2;
    const float BulletBaseSpeed = 2.4f;
    const float BulletSpeedModifier = 0.8f;
    const float BulletRotationSpeed = 45f;
    const float BulletRotationDuration = 0.5f;

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
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        int d = iii % 2 * 2 - 1;
                        float z = (i % 2 * 2 - 1) * ii * BranchSpacing;
                        float s = BulletBaseSpeed + (iii * BulletSpeedModifier);
                        Vector3 pos = Vector3.zero;

                        bulletData.colour = bulletData.gradient.Evaluate(iii);

                        var bullet = SpawnProjectile(0, z, pos);
                        bullet.StartCoroutine(bullet.RotateBy(d * BulletRotationSpeed, BulletRotationDuration));
                        bullet.StartCoroutine(bullet.RotateBy(d * BulletRotationSpeed * 2f, 0f, false, delay: 1f));
                        bullet.StartCoroutine(bullet.LerpSpeed(0f, s, 0.1f, delay: 1f));
                        bullet.Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(5f);
        }

    }
}