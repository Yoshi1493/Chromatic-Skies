using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBulletSystem1 : EnemyShooter<EnemyBullet>
{
    public const int WaveCount = 40;
    public const float WaveSpacing = 12f;
    public const int BranchCount = 12;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpawnRadius = 1.5f;
    const float BulletSpawnRadiusModifier = BulletSpawnRadius * 0.2f;
    const float BulletBaseSpeed = 2.4f;
    const float BulletSpeedModifier = 0.02f;

    List<EnemyBullet> bullets = new(WaveCount * BranchCount);

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            bullets.Clear();

            float r = Random.Range(0f, BranchSpacing);

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float t = (i * WaveSpacing) + (ii * BranchSpacing);
                        float s = BulletBaseSpeed + (i * BulletSpeedModifier);
                        float z = t + (iii * BulletSpacing) + i + r;
                        Vector3 pos = (Mathf.PingPong(i * BulletSpawnRadiusModifier, BulletSpawnRadius) + 0.5f) * transform.up.RotateVectorBy(t);

                        bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                        var bullet = SpawnProjectile(0, z, pos);
                        bullet.StartCoroutine(bullet.LerpSpeed(s, 2f, 3f));
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(15f);
        }
    }
}