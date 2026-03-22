using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesShooter010 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 8;
    const float WaveSpacing = -3f;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletSpacing = 45f;
    const float BulletSpawnRadius = 0.2f;
    const float SpawnRadiusModifier = 0.4f;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.3f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float t = ii * BranchSpacing;

                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = t + ((iii % 2 * 2 - 1) * (BulletSpacing + (i * WaveSpacing)));
                    float s = BulletBaseSpeed + (i * BulletSpeedModifier);
                    Vector3 pos = (BulletSpawnRadius + (i * SpawnRadiusModifier)) * transform.up.RotateVectorBy(t);

                    bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                    var bullet = SpawnProjectile(9, z, pos);
                    bullet.StartCoroutine(bullet.LerpSpeed(s, 0f, 0.5f));
                    bullet.Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}