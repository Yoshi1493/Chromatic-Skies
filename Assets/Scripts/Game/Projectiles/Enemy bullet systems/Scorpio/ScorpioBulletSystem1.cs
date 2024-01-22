using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 30;
    const int BranchCount = 12;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletRotationSpeed = 360f;
    public const float BulletRotationDuration = 3f;
    const float BulletRotationNoise = 15f;

    List<EnemyBullet> bullets = new(WaveCount * BranchCount);

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            bullets.Clear();

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = (ii + 0.5f) * BranchSpacing;
                        float r = (iii % 2 * -2 + 1) * BulletRotationSpeed;
                        Vector3 pos = Vector3.zero;

                        bulletData.colour = bulletData.gradient.Evaluate(iii / (BulletCount - 1f));

                        var bullet = SpawnProjectile(0, z, pos);
                        bullet.StartCoroutine(bullet.RotateBy(r, BulletRotationDuration));
                        bullets.Add(bullet);
                        bullet.Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            for (int i = 0; i < WaveCount; i++)
            {
                float r = (i / (float)WaveCount) * Random.Range(-BulletRotationNoise, BulletRotationNoise);

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        int b = (i * BranchCount * BulletCount) + (ii * BulletCount) + iii;
                        r *= iii % 2 * -2 + 1;

                        bullets[b].StartCoroutine(bullets[b].RotateBy(r, 0.2f));
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(5f);
        }
    }
}