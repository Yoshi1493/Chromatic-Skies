using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class LeoBulletSystem6 : EnemyShooter<EnemyBullet>
{
    const int BulletMinCount = 3;
    const int BulletMaxCount = 7;
    const float BulletSpawnRadius = 12f;

    protected override float ShootingCooldown => 0.3f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        //SetSubsystemEnabled(1);

        while (enabled)
        {
            int bulletCount = Random.Range(BulletMinCount, BulletMaxCount);

            for (int i = 0; i < bulletCount; i++)
            {
                float z = RandomAngleDeg;
                Vector3 pos = BulletSpawnRadius * Vector3.up.RotateVectorBy(z);

                SpawnProjectile(0, z, pos).Fire();
                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(ShootingCooldown * 2f);

        }
    }
}