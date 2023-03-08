using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem51 : EnemyShooter<EnemyBullet>
{
    const int MinBulletCount = 3;
    const int MaxBulletCount = 7;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        while (enabled)
        {
            int bulletCount = Random.Range(MinBulletCount, MaxBulletCount);

            for (int i = 0; i < bulletCount; i++)
            {
                float x = 0.8f * Random.Range(-screenHalfWidth, screenHalfWidth);
                float y = 1.1f * -screenHalfHeight;
                float z = 180f;
                Vector3 pos = new(x, y);

                SpawnProjectile(2, z, pos, false).Fire();
                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);
        }
    }
}