using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem61 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 4;
    const float BulletSpacing = 360f / BulletCount;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        float r = Random.Range(0f, BulletSpacing);

        for (int i = 0; i < BulletCount; i++)
        {
            float z = (i * BulletSpacing) + r;
            Vector3 pos = Vector3.zero;

            SpawnProjectile(1, z, pos).Fire();
        }

        enabled = false;
    }
}