using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class VirgoBulletSystem61 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 6;
    const float BulletSpacing = 360f / BulletCount;

    protected override IEnumerator Shoot()
    {
        yield return null;

        float r = PositiveOrNegativeOne;

        for (int i = 0; i < BulletCount; i++)
        {
            float z = (i * BulletSpacing) * r;

            bulletData.colour = bulletData.gradient.Evaluate(i / (BulletCount - 1f));
            SpawnProjectile(1, z, Vector3.zero).Fire();

        }

        enabled = false;
    }
}