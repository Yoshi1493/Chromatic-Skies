using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem41 : EnemyShooter<EnemyBullet>
{
    const float SpawnMaxAngle = 15f;
    const int BulletCount = 4;
    const float BulletMinSpeed = 4f;
    const float BulletMaxSpeed = 6f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        float y = screenHalfHeight * 1.1f;

        for (float i = 0f; enabled; i += 0.2f)
        {
            float z = Mathf.PingPong(i + SpawnMaxAngle, SpawnMaxAngle * 2f) - SpawnMaxAngle;

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float x = 1.5f * Random.Range(-screenHalfWidth, screenHalfWidth);
                float s = Random.Range(BulletMinSpeed, BulletMaxSpeed);
                Vector3 pos = new(x, y);

                bulletData.colour = bulletData.gradient.Evaluate(Mathf.InverseLerp(BulletMinSpeed, BulletMaxSpeed, s));

                var bullet = SpawnProjectile(1, z, pos);
                bullet.MoveSpeed = s;
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}