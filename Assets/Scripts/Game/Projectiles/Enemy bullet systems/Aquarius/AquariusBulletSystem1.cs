using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 18;
    const float WaveSpacing = 12f;
    const int BranchCount = 4;
    const float BranchSpacing = 15f;
    const int BulletCount = 2;
    const float BulletBaseSpeed = 3f;
    const float BulletSpeedMultiplier = -0.2f;
    const float BulletRotationSpeed = -90f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = (iii % 2 * 2 - 1) * ((i * WaveSpacing) - (ii * BranchSpacing) + 180f);
                        float s = BulletBaseSpeed + (ii * BulletSpeedMultiplier);
                        Vector3 pos = Vector3.zero;

                        bulletData.colour = bulletData.gradient.Evaluate(iii % 2);

                        var bullet = SpawnProjectile(0, z, pos);
                        bullet.StartCoroutine(bullet.LerpSpeed(s, s * 0.5f, 3f));
                        bullet.StartCoroutine(bullet.RotateBy((iii % 2 * 2 - 1) * BulletRotationSpeed, 10f));
                        bullet.Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            SetSubsystemEnabled(1);
            yield return WaitForSeconds(2f);

            StartMoveAction?.Invoke();

            yield return WaitForSeconds(3f);
        }
    }
}