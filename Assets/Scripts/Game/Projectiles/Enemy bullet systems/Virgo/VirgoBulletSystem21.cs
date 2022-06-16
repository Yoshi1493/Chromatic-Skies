using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem21 : EnemyBulletSubsystem<EnemyBullet>
{
    const int WaveCount = 3;
    const float WaveSpacing = 12f;
    const int BulletCount = 30;
    const float BulletSpacing = 360f / BulletCount;

    protected override float ShootingCooldown => 0.6f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float z = (i * WaveSpacing) + (ii * BulletSpacing);

                    SpawnProjectile(1, z, Vector2.zero).Fire();
                    SpawnProjectile(2, z, Vector2.zero).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(ShootingCooldown * 5f);
        }
    }
}