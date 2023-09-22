using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem6 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 120;
    const int BulletCount = 3;
    const float SpawnAngleVariance = 45f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            StartMoveAction?.Invoke();
            SetSubsystemEnabled(1);

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float z = 180f + Random.Range(-SpawnAngleVariance, SpawnAngleVariance);
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(0, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1.5f);

            SetSubsystemEnabled(2);
            yield return WaitForSeconds(4f);
        }
    }
}