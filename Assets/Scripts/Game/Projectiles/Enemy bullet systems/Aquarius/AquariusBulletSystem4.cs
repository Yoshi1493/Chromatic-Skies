using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const float ArcHalfWidth = 30f;
    const int BulletCount = 99;
    const float BulletSpawnRadius = 0.1f;

    protected override float ShootingCooldown => 1 / 60f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        for (int i = 0; i < BulletCount; i++)
        {
            float z = 180f + Random.Range(-ArcHalfWidth, ArcHalfWidth);
            Vector3 pos = BulletSpawnRadius * Random.insideUnitCircle;

            SpawnProjectile(0, z, pos).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }

        SetSubsystemEnabled(1);
        //SetSubsystemEnabled(2);
    }
}