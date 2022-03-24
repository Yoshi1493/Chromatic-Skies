using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem11 : EnemyBulletSubsystem<EnemyBullet>
{
    const int BulletCount = 8;
    const int Spacing = 360 / BulletCount;

    protected override IEnumerator Shoot()
    {
        int i = 0;

        while (enabled)
        {
            for (int j = 0; j < BulletCount; j++)
            {
                float z = (i * BulletCount) + (j * Spacing);
                SpawnProjectile(0, z, transform.up.RotateVectorBy(z) * Mathf.PingPong(i * 0.05f, 1f)).Fire();
            }

            i++;
            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}