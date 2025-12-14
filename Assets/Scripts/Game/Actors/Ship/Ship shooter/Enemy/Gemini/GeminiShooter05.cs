using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiShooter05 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 4;
    const int BulletCount = 16;
    const float BulletSpacing = 360f / BulletCount;

    protected override float ShootingCooldown => 1.2f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            yield return WaitForSeconds(ShootingCooldown);

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = ii * BulletSpacing;
                Vector3 pos = Vector3.zero;

                var bullet = SpawnProjectile(5, z, pos);
                bullet.StartCoroutine(bullet.LerpSpeed(2f, 0f, 1f));
                bullet.Fire();
            }
        }
    }
}