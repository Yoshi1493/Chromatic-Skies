using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerShooter01 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 66;
    const int BulletMinCount = 3;
    const int BulletMaxCount = 6;
    const float BulletSpawnRadius = 0.5f;

    protected override float ShootingCooldown => 1f / 30;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        for (int i = 0; i < WaveCount; i++)
        {
            int bulletCount = Random.Range(BulletMinCount, BulletMaxCount);

            for (int ii = 0; ii < bulletCount; ii++)
            {
                float z = PlayerPosition.GetRotationDifference(transform.position) + Random.Range(-60f, 60f);
                Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(z);

                SpawnProjectile(1, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}