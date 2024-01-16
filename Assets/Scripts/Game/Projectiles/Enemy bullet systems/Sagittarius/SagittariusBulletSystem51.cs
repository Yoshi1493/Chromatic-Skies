using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBulletSystem51 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 50;
    const float WaveSpacing = 5f;
    const float WaveSpeedModifier = 0.02f;
    const int BranchCount = 3;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 3;
    const float BulletSpacing = 10f;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 1f;
    const float BulletSpawnRadius = 0.5f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        float d = Mathf.Sign(transform.position.x - PlayerPosition.x);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = d * ((i * WaveSpacing) + (ii * BranchSpacing) + (iii * BulletSpacing) - 90f);
                    float s = BulletBaseSpeed + (i * WaveSpeedModifier) + (iii + BulletSpeedModifier);
                    Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(z - (d * BranchSpacing));

                    bulletData.colour = bulletData.gradient.Evaluate((iii) / (BulletCount - 1f));

                    var bullet = SpawnProjectile(2, z, pos);
                    bullet.StartCoroutine(bullet.LerpSpeed(0f, s, 1f));
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}