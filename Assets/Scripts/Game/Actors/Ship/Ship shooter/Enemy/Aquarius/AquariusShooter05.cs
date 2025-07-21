using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class AquariusShooter05 : EnemyShooter<EnemyBullet>
{
    const int SmallWaveCount = 5;
    const float SmallWaveSpacing = 6f;
    const int SmallBranchCount = 24;
    const float SmallBranchSpacing = 360f / SmallBranchCount;
    const float SmallBulletBaseSpeed = 0.5f;
    const float SmallBulletSpeedModifier = 2f;

    const int LargeWaveCount = 3;
    const int LargeBranchCount = 20;
    const float LargeBranchSpacing = 360f / LargeBranchCount;
    const int LargeBulletCount = 2;
    const float LargeBulletRotationSpeed = 90f;
    const float LargeBulletRotationDuration = 9f;

    [SerializeField] ProjectileObject largeBulletData;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        for (int i = 1; enabled; i *= -1)
        {
            Vector3 pos = Vector3.zero;

            for (int ii = 0; ii < SmallWaveCount; ii++)
            {
                bulletData.colour = bulletData.gradient.Evaluate(ii / (SmallWaveCount - 1f));
                float s = SmallBulletBaseSpeed + (ii * SmallBulletSpeedModifier);

                for (int iii = 0; iii < SmallBranchCount; iii++)
                {
                    float z = i * ((ii * SmallWaveSpacing) + (iii * SmallBranchSpacing));

                    var bullet = SpawnProjectile(5, z, pos);
                    bullet.MoveSpeed = s;
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(3.5f);

            for (int ii = 0; ii < LargeWaveCount; ii++)
            {
                for (int iii = 0; iii < LargeBranchCount; iii++)
                {
                    for (int iv = 0; iv < LargeBulletCount; iv++)
                    {
                        float z = iii * LargeBranchSpacing;

                        largeBulletData.colour = largeBulletData.gradient.Evaluate(iv);

                        var bullet = SpawnProjectile(6, z, pos);
                        bullet.StartCoroutine(bullet.RotateBy((iv % 2 * 2 - 1) * LargeBulletRotationSpeed, LargeBulletRotationDuration));
                        bullet.MoveSpeed = 2f;
                    }
                }

                yield return WaitForSeconds(1f);
            }

        }
    }
}