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

        int i = 0;

        while (enabled)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float offset = Mathf.PingPong(i, 30f) - RotationPerWave;
                float z = (ii * BulletSpacing) + (i * offset) + 180f;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(0, z, pos).Fire();
                yield return WaitForSeconds(ShootingCooldown);
            }

            i++;
        }
    }
}