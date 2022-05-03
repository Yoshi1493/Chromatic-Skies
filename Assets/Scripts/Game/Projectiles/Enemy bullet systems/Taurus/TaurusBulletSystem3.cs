using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 3;
    const int BulletSpacing = 360 / BulletCount;
    const float RotationPerWave = 15f;

    protected override float ShootingCooldown => 0.03f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        int ctr = 0;

        while (enabled)
        {
            for (int i = 0; i < BulletCount; i++)
            {
                float offset = Mathf.PingPong(ctr, 30f) - RotationPerWave;
                float z = (i * BulletSpacing) + (ctr * offset) + 180f;

                SpawnProjectile(0, z, Vector3.zero).Fire();
                yield return WaitForSeconds(ShootingCooldown);
            }

            ctr++;
        }
    }
}