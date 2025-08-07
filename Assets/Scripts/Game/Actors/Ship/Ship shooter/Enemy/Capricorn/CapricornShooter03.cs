using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornShooter03 : CommonEnemyShooter<EnemyBullet>
{
    const int BulletCount = 5;
    const float BulletSpacing = 20f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        float r = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < BulletCount; i++)
        {
            float z = ((i - ((BulletCount - 1) / 2f)) * BulletSpacing) + r;
            Vector3 pos = Vector3.zero;

            SpawnProjectile(0, z, pos).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}