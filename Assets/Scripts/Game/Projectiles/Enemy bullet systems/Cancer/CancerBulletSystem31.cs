using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem31 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 24;
    const float BulletSpawnRadius = 0.5f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            float z = PlayerPosition.GetRotationDifference(transform.position);
            Vector3 pos = BulletSpawnRadius * Random.insideUnitCircle;
            SpawnProjectile(4, z, pos).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}