using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static CameraBoundaries;

public class PiscesShooter07 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 20;
    const int BulletMinCount = 2;
    const int BulletMaxCount = 6;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        for (int i = 1; enabled; i *= -1)
        {
            yield return WaitForSeconds(1f);

            for (int ii = 0; ii < WaveCount; ii++)
            {
                int bulletCount = Random.Range(BulletMinCount, BulletMaxCount);
                float t = ii / (WaveCount - 1f);

                for (int iii = 0; iii < bulletCount; iii++)
                {
                    float z = 0f;
                    float x = i * Mathf.Lerp(-ScreenHalfWidth, ScreenHalfWidth, t) + Random.Range(-1f, 1f);
                    float y = ScreenHalfHeight * 1.1f;
                    Vector3 pos = new(x, y);

                    bulletData.colour = bulletData.gradient.Evaluate(t);

                    SpawnProjectile(7, z, pos, false).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}