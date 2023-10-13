using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem42 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const float WaveSpacing = BulletSpacing / WaveCount;
    const int BulletCount = 12;
    const float BulletSpacing = 360f / (BulletCount);
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedMultiplier = 0.5f;

    protected override float ShootingCooldown => 5f;

    protected override IEnumerator Shoot()
    {
        for (int i = 1; enabled; i *= -1)
        {
            yield return WaitForSeconds(ShootingCooldown);

            for (int ii = 0; ii < WaveCount; ii++)
            {
                float r = ii % WaveCount;

                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = i * ((ii * WaveSpacing) + (iii * BulletSpacing));
                    float s = BulletBaseSpeed + (r * BulletSpeedMultiplier);
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(ii / (WaveCount - 1f));
                    var bullet = SpawnProjectile(2, z, pos);
                    bullet.MoveSpeed = s;
                    bullet.Fire();
                }
            }
        }

    }
}