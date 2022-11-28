using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 9;
    const int BulletSpacing = 360 / BulletCount;

    protected override float ShootingCooldown => 0.6f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();
        SetSubsystemEnabled(1);

        while (enabled)
        {
            float r = Random.Range(0f, BulletSpacing);

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (ii * BulletSpacing) + r;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(0, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}