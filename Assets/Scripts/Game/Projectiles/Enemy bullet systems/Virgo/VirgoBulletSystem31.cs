using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem31 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 20;
    const float BulletSpacing = 360f / BulletCount;

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            float r = Random.Range(0f, BulletSpacing);

            for (int i = 0; i <= BulletCount; i++)
            {
                float z = (i * BulletSpacing) + r;
                SpawnProjectile(1, z, Vector3.zero).Fire();

            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}