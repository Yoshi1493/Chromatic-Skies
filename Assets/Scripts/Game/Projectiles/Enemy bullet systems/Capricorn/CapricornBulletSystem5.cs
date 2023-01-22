using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 7;
    const int BulletCount = 48;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpeed = 2.5f;
    const float BulletSpeedMultiplier = 0.2f;

    protected override float ShootingCooldown => 0.3f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            SetSubsystemEnabled(1);

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float z = ii * BulletSpacing;
                    float s = -i * BulletSpeedMultiplier + BulletSpeed;
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                    var bullet = SpawnProjectile(0, z, pos);
                    bullet.MoveSpeed = s;
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(3f);

            StartMoveAction?.Invoke();
            SetSubsystemEnabled(2);

            yield return WaitForSeconds(3f);
        }
    }
}