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
            float r = Random.Range(0f, BulletSpacing);
            Vector3 pos = Vector3.zero;

            for (int i = 0; i < BulletCount; i++)
            {
                float z = (i * BulletSpacing) + r;
                SpawnProjectile(1, z, pos).Fire();
            }

            yield return WaitForSeconds(0.4f);

            for (int i = 0; i < BulletCount; i++)
            {
                float z = (i * BulletSpacing) + r;

                SpawnProjectile(2, z, pos).Fire();
            }

            enabled = false;
        }
    }
}