using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem31 : EnemyShooter<EnemyBullet>
{
    const int RepeatCount = 6;
    const int WaveCount = 12;
    const int BranchCount = 2;
    const float BulletSpawnAngle = 45f;
    const float SpawnAngleModifier = 15f;
    const float BulletBaseSpeed = 5f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        for (int i = 0; i < RepeatCount; i++)
        {
            float z = BulletSpawnAngle + (i * SpawnAngleModifier);
            StartCoroutine(SpawnBullets(z));

            yield return WaitForSeconds(0.4f);
        }

        enabled = false;
    }

    IEnumerator SpawnBullets(float zRotation)
    {
        List<EnemyBullet> bullets = new(WaveCount * BranchCount);
        Vector3 pos = transform.position;

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                int d = ii % 2 * 2 - 1;
                float z = d * zRotation;                

                bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                var bullet = SpawnProjectile(1, z, pos, false);
                bullet.StartCoroutine(bullet.LerpSpeed(BulletBaseSpeed, 0f, 1f));
                bullets.Add(bullet);
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        yield return WaitForSeconds(1f);

        bullets.ForEach(b => b.LookAt(PlayerPosition));

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                int b = (i * BranchCount) + ii;
                bullets[b].Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}