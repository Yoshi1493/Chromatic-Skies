using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 0;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < BulletCount; i++)
            {
                float z = 0f;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(0, z, pos).Fire();
                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}