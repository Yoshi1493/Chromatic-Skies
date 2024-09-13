using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBulletSystem51 : EnemyShooter<EnemyBullet>
{
    const int RepeatCount = 2;
    const float RepeatSpacing = BranchSpacing / 2f;
    const int WaveCount = 25;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpawnRadius = 1.5f;
    const float BulletRotationSpeed = 45f;
    const float BulletRotationDuration = 3f;

    protected override float ShootingCooldown => 0.05f;

    List<EnemyBullet> bullets = new(WaveCount * BranchCount);

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3f);

        for (int i = 0; i < RepeatCount; i++)
        {
            bullets.Clear();

            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    int b = 2 + (iii % 2);
                    float z = Mathf.Lerp(-150f, 150f, ii / (WaveCount - 1f)) + (iii * BranchSpacing);
                    float r = (i * RepeatSpacing) + (iii * BranchSpacing);
                    Vector3 pos = (BulletSpawnRadius * transform.up.RotateVectorBy(r)) + (BulletSpawnRadius * 0.5f * transform.up.RotateVectorBy(z));

                    var bullet = SpawnProjectile(b, z + 180f, pos);
                    bullet.StartCoroutine(bullet.RotateBy((iii % 2 * 2 - 1) * BulletRotationSpeed, BulletRotationDuration, delay: 2f));
                    bullets.Add(bullet);
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);
            bullets.ForEach(b => b.Fire());
        }

        enabled = false;
    }
}