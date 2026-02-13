using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoShooter02 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 18;
    const float BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpawnRadius = 0.5f;
    const float BulletBaseSpeed = 3f;
    const float BulletRotationSpeed = 10f;
    const float BulletSpeedDelay = 0.5f;
    const float BulletSpeedDelayModifier = 0.1f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(Random.Range(1f, 2f));

        float r = (BranchSpacing / 2f) + (BranchSpacing / 4f * Random.Range(-1f, 1f));

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = i * BulletRotationSpeed;
                float d = BulletSpeedDelay + (i * BulletSpeedDelayModifier);
                Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy((ii * BranchSpacing) + r);

                bulletData.colour = bulletData.gradient.Evaluate(ii);

                var bullet = SpawnProjectile(2, z, pos);
                bullet.MoveSpeed = 0f;
                bullet.StartCoroutine(bullet.LerpSpeed(0f, BulletBaseSpeed, 1f, delay: d));
                bullet.StartCoroutine(bullet.RotateBy(-z, d));
            }
        }
    }
}