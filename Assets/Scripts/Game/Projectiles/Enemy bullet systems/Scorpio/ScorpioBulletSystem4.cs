using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 8;
    const int BranchCount = 24;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletSpacing = 3f;
    const float BulletSpawnRadius = 1.5f;
    const float BulletBaseSpeed = 6f;
    const float BulletSpeedModifier = 0.5f;

    List<EnemyBullet> bullets = new(WaveCount * BranchCount * BulletCount);

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            bullets.Clear();

            for (int i = 0; i < WaveCount; i++)
            {
                Vector3 pos = BulletSpawnRadius * Random.insideUnitCircle;

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = (ii * BranchSpacing) + (iii * BulletSpacing);
                        float s = BulletBaseSpeed + (iii * BulletSpeedModifier);

                        bulletData.colour = bulletData.gradient.Evaluate(iii);

                        var bullet = SpawnProjectile(1, z, pos);
                        bullet.MoveSpeed = s;
                        bullet.Fire();
                        bullets.Add(bullet);
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(3f);

            SpawnProjectile(0, 0f, Vector3.zero).Fire();
            yield return WaitForSeconds(2.5f);

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        int b = (i * BranchCount * BulletCount) + (ii * BulletCount) + iii;

                        if (bullets[b].isActiveAndEnabled)
                        {
                            bullets[b].GetComponent<ITimestoppable>().Resume();
                        }
                    }
                }

                yield return WaitForSeconds(ShootingCooldown * 2f);
            }

            yield break;
        }
    }
}