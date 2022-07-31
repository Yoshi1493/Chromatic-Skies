using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem11 : EnemyBulletSubsystem<EnemyBullet>
{
    const int BulletCount = 8;
    const int Spacing = 360 / BulletCount;
    const float MaxRadius = 1f;
    const float RadiusScaling = 0.05f;

    protected override IEnumerator Shoot()
    {
        int i = 0;

        while (enabled)
        {
            for (int j = 0; j < BulletCount; j++)
            {
                float z = (i * BulletCount) + (j * Spacing);
                Vector3 pos = Mathf.PingPong(i * RadiusScaling, MaxRadius) * transform.up.RotateVectorBy(z);

                SpawnProjectile(0, z, pos).Fire();
            }

            i++;
            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}