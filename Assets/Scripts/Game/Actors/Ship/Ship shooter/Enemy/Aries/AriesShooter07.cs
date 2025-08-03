using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class AriesShooter07 : CommonEnemyShooter<EnemyBullet>
{
    const int BulletCount = 4;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpawnRadius = 0.2f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        yield return WaitUntil(() => parentShip.HealthPercent < 0.5f);

        while (enabled)
        {
            float z = RandomAngleDeg;
            bulletData.colour = bulletData.gradient.Evaluate(Random.value);

            for (int i = 0; i < BulletCount; i++)
            {
                Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(i * BulletSpacing);

                SpawnProjectile(7, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}