using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesShooter06 : CommonEnemyShooter<EnemyBullet>
{
    const int BulletCount = 45;
    const float BulletSpacing = 360f / BulletCount;
    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        yield return WaitUntil(() => parentShip.HealthPercent < 0.5f);

        for (int i = 0; enabled; i++)
        {
            yield return WaitForSeconds(ShootingCooldown);
            int d = i % 2;

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = ii * BulletSpacing;
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(d);

                var bullet = SpawnProjectile(6, z, pos) as AriesBullet06;
                bullet.rotatesClockwise = d == 0;
                bullet.Fire();
            }
        }
    }
}