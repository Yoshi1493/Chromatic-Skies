using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class CapricornShooter07 : CommonEnemyShooter<EnemyBullet>
{
    const int BulletMinCount = 3;
    const int BulletMaxCount = 6;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(6.5f);

        while (enabled)
        {
            int bulletCount = Random.Range(BulletMinCount, BulletMaxCount);

            for (int ii = 0; ii < bulletCount; ii++)
            {
                float z = RandomAngleDeg;
                Vector3 pos = Random.insideUnitCircle;

                SpawnProjectile(7, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

    }
}