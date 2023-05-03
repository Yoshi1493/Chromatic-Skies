using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const float ArcHalfWidth = 60f;
    const int BulletCount = 2;
    const float BulletSpawnRadius = 1.5f;
    const float BulletSpawnOffset = 2.5f;
    const float BulletMinSpeed = 2f;
    const float BulletMaxSpeed = 3f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            for (int i = 0; i < BulletCount; i++)
            {
                float z = Random.Range(-ArcHalfWidth, ArcHalfWidth);
                float s = Random.Range(BulletMinSpeed, BulletMaxSpeed);
                Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(z) + (BulletSpawnOffset * Vector3.up);

                bulletData.colour = bulletData.gradient.Evaluate(Mathf.InverseLerp(BulletMinSpeed, BulletMaxSpeed, s));

                var bullet = SpawnProjectile(1, z, pos, false);
                bullet.MoveSpeed = s;
                bullet.Fire();
            }
            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}