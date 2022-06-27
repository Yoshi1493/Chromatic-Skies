using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem21 : EnemyBulletSubsystem<EnemyBullet>
{
    const float WaveSpacing = 12f;
    const int BulletCount = 30;
    const float BulletSpacing = 360f / BulletCount;

    protected override float ShootingCooldown => 1.2f;

    protected override IEnumerator Shoot()
    {
        int i = 0;

        while (enabled)
        {
            yield return WaitForSeconds(ShootingCooldown);

            int p = i % 2 + 1;

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (i * WaveSpacing) + (ii * BulletSpacing);
                SpawnProjectile(p, z, Vector2.zero).Fire();
            }

            i++;
        }
    }
}