using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoShooter04 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 16;
    const float WaveSpacing = 360f / WaveCount;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 8;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpawnRadius = 0.16f;
    const float BulletBaseSpeed = 2f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2.2f);

        for (int i = 0; i < WaveCount; i++)
        {
            bulletData.colour = bulletData.gradient.Evaluate(Random.value);

            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (i * WaveSpacing) + (ii * BranchSpacing) + PlayerPosition.GetRotationDifference(transform.position);

                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float t = (iii * BulletSpacing) + z;
                    Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(t);

                    var bullet = SpawnProjectile(4, z, pos);
                    bullet.StartCoroutine(bullet.LerpSpeed(0f, BulletBaseSpeed, 2f));
                    bullet.StartCoroutine(bullet.RotateBy(-iii * BulletSpacing, 0f, delay: 0.5f));
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}