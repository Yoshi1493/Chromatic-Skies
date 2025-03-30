using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem1 : BossShooter<BossBullet>
{
    const int RepeatCount = 2;
    const int WaveCount = 3;
    const float WaveSpacing = 8f;
    const int BulletCount = 60;
    const float BulletSpacing = 360f / BulletCount;
    const int BulletClumpCount = 3;
    const float BulletClumpSpacing = 3f;
    const float BulletRotationSpeed = 15f;
    const float BulletRotationDuration = 6f;

    protected override float ShootingCooldown => 0.4f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            StartMoveAction?.Invoke();
            SetSubsystemEnabled(1);

            for (int i = 0; i < RepeatCount; i++)
            {
                for (int ii = 0; ii < WaveCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = (i % 2 * 2 - 1) * ((ii * WaveSpacing) + (iii * (BulletSpacing - BulletClumpSpacing)) + (iii - (iii % BulletClumpCount)) * BulletClumpSpacing);
                        Vector3 pos = Vector3.zero;

                        bulletData.colour = bulletData.gradient.Evaluate(i);

                        var bullet = SpawnProjectile(0, z, pos);
                        bullet.StartCoroutine(bullet.RotateBy(i * BulletRotationSpeed, BulletRotationDuration));
                        bullet.Fire();
                    }

                    yield return WaitForSeconds(ShootingCooldown);
                }

                yield return WaitForSeconds(1f);
            }

            yield return WaitForSeconds(3f);
        }
    }
}