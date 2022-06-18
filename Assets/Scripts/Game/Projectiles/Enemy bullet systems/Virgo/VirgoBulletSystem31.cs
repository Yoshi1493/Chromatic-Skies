using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem31 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 15;
    const float BulletSpacing = 360f / BulletCount;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return WaitForSeconds(3f);

            float r = Random.Range(0f, BulletSpacing);

            for (int i = 0; i < BulletCount; i++)
            {
                float z = (i * BulletSpacing) + r;
                SpawnProjectile(1, z, Vector3.zero).Fire();
                SpawnProjectile(2, z, Vector3.zero).Fire();
            }

            yield return WaitForSeconds(2f);
        }
    }
}