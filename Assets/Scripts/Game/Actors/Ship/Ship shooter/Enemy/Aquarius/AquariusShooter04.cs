using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusShooter04 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const int BulletCount = 3;
    const float BulletSpacing = 15f;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.6f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        float r = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = ((ii - (BulletCount - 1) / 2) * BulletSpacing) + r;
                float s = BulletBaseSpeed + (i * BulletSpeedModifier);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                var bullet = SpawnProjectile(4, z, pos);
                bullet.MoveSpeed = s;
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

    }
}