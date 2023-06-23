using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 24;
    const float BulletSpacing = 360f / BulletCount;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < BulletCount; i++)
            {
                float z = i * BulletSpacing;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(0, z, pos).Fire();
            }

            yield return WaitForSeconds(10f);
        }
    }
}