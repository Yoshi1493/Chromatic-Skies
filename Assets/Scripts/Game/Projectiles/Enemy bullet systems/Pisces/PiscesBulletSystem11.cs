using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem11 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 4;
    const float WaveSpacing = BulletSpacing / WaveCount;
    const int BulletCount = 48;
    const int BulletSpacing = 360 / BulletCount;
    const float BulletBaseSpeed = 1.5f;
    const float BulletSpeedModifier = 2f;

    protected override float ShootingCooldown => 0.5f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float r = Random.value;

                int b = Mathf.RoundToInt(Random.value) + 1;
                float z = (i * WaveSpacing) + (ii * BulletSpacing);
                float s = BulletBaseSpeed + (r * BulletSpeedModifier);

                bulletData.colour = bulletData.gradient.Evaluate(r);

                var bullet = SpawnProjectile(b, z, Vector3.zero);
                bullet.MoveSpeed = s;
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}