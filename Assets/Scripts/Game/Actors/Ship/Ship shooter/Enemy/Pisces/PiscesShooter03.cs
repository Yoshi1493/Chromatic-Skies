using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesShooter03 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 16;
    const int BulletCount = 3;
    const float BulletSpawnRadius = 0.5f;
    const float BulletRotationSpeed = 5f;
    const float BulletRotationDuration = 1f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        for (int i = 0; i < WaveCount; i++)
        {
            float z = PlayerPosition.GetRotationDifference(transform.position);
            Vector3 pos = BulletSpawnRadius * Random.insideUnitCircle;

            for (int ii = 0; ii < BulletCount; ii++)
            {
                var bullet = SpawnProjectile(3, z, pos);
                bullet.StartCoroutine(bullet.RotateBy(Random.Range(-BulletRotationSpeed, BulletRotationSpeed), BulletRotationDuration, delay: 1f));
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}