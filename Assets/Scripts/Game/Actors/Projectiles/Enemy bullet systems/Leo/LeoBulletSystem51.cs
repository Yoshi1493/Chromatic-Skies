using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem51 : EnemyShooter<EnemyBullet>
{
    const float WaveSpacing = 100f;
    const int BulletClumpCount = 5;
    const float BulletSpacing = 5f;
    const float BulletRotationSpeedModifier = -15f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; enabled; i++)
        {
            for (int ii = 0; ii < BulletClumpCount; ii++)
            {
                float r = ii - ((BulletClumpCount - 1) / 2f);
                float z = (i * WaveSpacing) + (r * BulletSpacing);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(ii / (BulletClumpCount - 1f));

                var bullet = SpawnProjectile(4, z, pos);
                bullet.StartCoroutine(bullet.RotateBy(r * BulletRotationSpeedModifier, 2f, delay: 1f));
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}