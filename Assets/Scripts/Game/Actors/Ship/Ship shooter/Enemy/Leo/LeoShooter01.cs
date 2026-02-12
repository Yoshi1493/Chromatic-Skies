using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoShooter01 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 20;
    const float BulletSpawnRadius = 2f;
    const float BulletBaseSpeed = 4f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.8f);

        float z = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < WaveCount; i++)
        {
            Vector3 pos = BulletSpawnRadius * Vector3.forward;

            bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

            var bullet = SpawnProjectile(1, z, pos) as LeoBullet01;
            bullet.rotationPoint = parentShip.transform.position;
            bullet.MoveSpeed = BulletBaseSpeed;
            bullet.Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}