using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem22 : EnemyShooter<EnemyBullet>
{
    const float WaveSpacing = 20f;
    const float BranchSpacing = 6f;
    const int BulletCount = 3;
    const float BulletSpacing = 30f;

    protected override IEnumerator Shoot()
    {
        int i = 0;

        while (enabled)
        {
            float t = (i + 0.5f) * WaveSpacing;

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (i * BranchSpacing) + ((ii - ((BulletCount - 1) / 2f)) * BulletSpacing);
                Vector3 pos = transform.up.RotateVectorBy(-t);

                SpawnProjectile(2, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
            i = (int)Mathf.Repeat(++i, 360f / BranchSpacing);
        }
    }
}