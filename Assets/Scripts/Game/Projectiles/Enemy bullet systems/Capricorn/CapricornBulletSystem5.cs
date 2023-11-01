using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 9;
    const float WaveSpacing = 6f;
    const int BulletCount = 32;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.25f;
    const float BulletRotationSpeed = 30f;
    const float BulletRotationSpeedModifier = 5f;

    protected override float ShootingCooldown => 1.5f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        List<EnemyBullet> bullets = new(WaveCount * BulletCount);

        for (int i = 1; enabled; i *= -1)
        {
            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = (ii * WaveSpacing) + (iii * BulletSpacing);
                    float s = BulletBaseSpeed + (ii * BulletSpeedModifier);
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(ii / (WaveCount - 1f));

                    var bullet = SpawnProjectile(0, z, pos);
                    bullet.MoveSpeed = s;
                    bullets.Add(bullet);
                }
            }

            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    int b = (ii * BulletCount) + iii;
                    float t = i * (BulletRotationSpeed + (ii * BulletRotationSpeedModifier));

                    bullets[b].StartCoroutine(bullets[b].RotateBy(t, 5f));
                    bullets[b].Fire();
                }
            }

            bullets.Clear();
            yield return WaitForSeconds(ShootingCooldown);

            SetSubsystemEnabled(1);
            yield return WaitForSeconds(ShootingCooldown);

            SetSubsystemEnabled(2);
            StartMoveAction?.Invoke();
            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}