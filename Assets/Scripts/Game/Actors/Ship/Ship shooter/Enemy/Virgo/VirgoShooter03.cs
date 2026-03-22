using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoShooter03 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 2;
    const int BulletCount = 16;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletRotationSpeed = 90f;
    const float BulletRotationDelay = 1f;
    const float BulletRotationDelayModifier = 0.1f;

    protected override float ShootingCooldown => 0.8f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = ii * BulletSpacing;
                float d = BulletRotationDelay + (ii * BulletRotationDelayModifier);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(i);

                var bullet = SpawnProjectile(3, z, pos);
                bullet.StartCoroutine(bullet.RotateBy((i % 2 * 2 - 1) * BulletRotationSpeed, 0f, delay: d));
                bullet.StartCoroutine(bullet.LerpSpeed(0f, 2f, 1f, delay: d));
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}