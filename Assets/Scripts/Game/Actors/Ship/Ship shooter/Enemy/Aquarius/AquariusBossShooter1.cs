using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBossShooter1 : BossShooter<BossBullet>
{
    const int WaveCount = 18;
    const float WaveSpacing = 12f;
    const int RingCount = 4;
    const float RingSpacing = 15f;
    const int BulletCount = 2;
    const float BulletBaseSpeed = 3f;
    const float BulletSpeedModifier = -0.2f;
    const float BulletRotationSpeed = -90f;
    const float BulletRotationDuration = 10f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < RingCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = (iii % 2 * 2 - 1) * ((i * WaveSpacing) - (ii * RingSpacing) + 180f);
                        float s = BulletBaseSpeed + (ii * BulletSpeedModifier);
                        Vector3 pos = Vector3.zero;

                        bulletData.colour = bulletData.gradient.Evaluate(iii % 2);

                        var bullet = SpawnProjectile(0, z, pos);
                        bullet.MoveSpeed = s;
                        bullet.StartCoroutine(bullet.RotateBy((iii % 2 * 2 - 1) * BulletRotationSpeed, BulletRotationDuration));
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