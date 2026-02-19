using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoShooter01 : CommonEnemyShooter<EnemyBullet>
{
    const int RepeatCount = 2;
    const int WaveCount = 20;
    const float BulletSpawnRadius = 2f;
    const float BulletBaseSpeed = 4f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.8f);

        float z = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < RepeatCount; i++)
        {
            for (int ii = 0; ii < WaveCount; ii++)
            {
                Vector3 pos = BulletSpawnRadius * Vector3.forward;

                bulletData.colour = bulletData.gradient.Evaluate(ii / (WaveCount - 1f));

                var bullet = SpawnProjectile(1, z, pos) as LeoBullet01;
                bullet.rotationPoint = parentShip.transform.position;
                bullet.MoveSpeed = BulletBaseSpeed;
                bullet.Fire();

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(3f);
        }
    }
}