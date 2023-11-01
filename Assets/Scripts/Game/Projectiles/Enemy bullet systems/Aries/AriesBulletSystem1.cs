using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 6;
    const float WaveSpacing = 15f;
    const int BranchCount = 6;
    const float BranchSpacing = 6f;
    const int BulletCount = 6;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletBaseSpeed = 4.8f;
    const float BulletSpeedModifier = -0.2f;
    const float BulletRotationSpeed = 90f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        for (int i = 1; enabled; i *= -1)
        {
            float r = i * BulletSpacing;

            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    for (int iv = 0; iv < BulletCount; iv++)
                    {
                        float z = i * (ii % 2 * 2 - 1) * ((ii * WaveSpacing) + (iii * BranchSpacing) + (iv * BulletSpacing));
                        float s = BulletBaseSpeed + (ii * BulletSpeedModifier);
                        Vector3 pos = Vector3.zero;

                        bulletData.colour = bulletData.gradient.Evaluate(ii % 2);

                        var bullet = SpawnProjectile(0, z, pos);
                        bullet.StartCoroutine(bullet.LerpSpeed(s, s * 0.5f, 0.5f));
                        bullet.StartCoroutine(bullet.RotateBy((ii % 2 * 2 - 1) * BulletRotationSpeed, 4f));
                        bullet.Fire();
                    }

                    yield return WaitForSeconds(ShootingCooldown * 0.5f);
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);

            StartMoveAction?.Invoke();

            yield return WaitForSeconds(3f);
        }
    }
}