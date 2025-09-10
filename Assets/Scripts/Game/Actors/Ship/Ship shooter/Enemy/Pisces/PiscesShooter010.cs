using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesShooter010 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 8;
    const float WaveSpacing = 2f;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletSpacing = 45f;
    const float BulletSpawnRadius = 0.2f;
    const float SpawnRadiusModifier = 0.4f;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.3f;
    const float BulletRotationSpeedModifier = 5f;
    const float BulletRotationDuration = 1f;

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
                    float z = (i * WaveSpacing) + t + ((iii % 2 * 2 - 1) * BulletSpacing);
                    float s = BulletBaseSpeed + (i * BulletSpeedModifier);
                    float r = (iii % 2 * 2 - 1) * (((WaveCount - 1) / 2f * -BulletRotationSpeedModifier) + (i * BulletRotationSpeedModifier));
                    Vector3 pos = (BulletSpawnRadius + (i * SpawnRadiusModifier)) * transform.up.RotateVectorBy(t);

                    bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                    var bullet = SpawnProjectile(9, z, pos);
                    bullet.StartCoroutine(bullet.LerpSpeed(s, 0f, 1f));
                    bullet.StartCoroutine(bullet.RotateBy(r, BulletRotationDuration, delay: 1f));
                    bullet.Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}