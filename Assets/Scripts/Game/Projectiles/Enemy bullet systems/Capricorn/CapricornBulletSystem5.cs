using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 9;
    const float WaveSpacing = 6f;
    const int BulletCount = 36;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedMultiplier = 0.3f;

    protected override float ShootingCooldown => 1.5f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float z = (i * WaveSpacing) + (ii * BulletSpacing);
                    float s = BulletBaseSpeed + (i * BulletSpeedMultiplier);
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                    var bullet = SpawnProjectile(0, z, pos);
                    bullet.MoveSpeed = s;
                    bullet.Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);

            SetSubsystemEnabled(1);
            yield return WaitForSeconds(ShootingCooldown);

            SetSubsystemEnabled(2);
            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}