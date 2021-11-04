using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem41 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            for (int i = 0; i < 88; i++)
            {
                float z = i * Mathf.Rad2Deg;
                SpawnProjectile(1, z, (i + 1) * 0.1f * transform.up.RotateVectorBy(z)).Fire();

                yield return WaitForSeconds(ShootingCooldown / 2f);
            }

            yield return WaitForSeconds(4.4f);

            for (int i = 0; i < 88; i++)
            {
                float z = i * -Mathf.Rad2Deg;
                SpawnProjectile(1, z, (i + 1) * 0.1f * transform.up.RotateVectorBy(z)).Fire();

                yield return WaitForSeconds(ShootingCooldown / 2f);
            }

            yield return WaitForSeconds(4.4f);
        }
    }
}