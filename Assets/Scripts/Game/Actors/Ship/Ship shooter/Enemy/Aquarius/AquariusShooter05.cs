using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusShooter05 : CommonEnemyShooter<EnemyBullet>
{
    const int SmallWaveCount = 5;
    const float SmallWaveSpacing = 6f;
    const int SmallBranchCount = 24;
    const float SmallBranchSpacing = 360f / SmallBranchCount;
    const float SmallBulletBaseSpeed = 0.5f;
    const float SmallBulletSpeedModifier = 2f;

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
        }
    }
}