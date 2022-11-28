using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 2;
    const float WaveSpacing = BulletSpacing / 2f;
    const int BulletCount = 9;
    const int BulletSpacing = 360 / BulletCount;

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
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(0, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown * 5f);
                yield return null;
            }
        }
    }
}