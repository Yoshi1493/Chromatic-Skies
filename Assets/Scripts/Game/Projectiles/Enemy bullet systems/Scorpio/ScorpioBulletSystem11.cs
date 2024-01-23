using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBulletSystem11 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 12;
    const float WaveSpacing = 360f / WaveCount;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 12;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpawnRadius = ScorpioBulletSystem1.BulletSpawnRadius;
    const float BulletBaseSpeed = 3.6f;
    const float BulletSpeedModifier = 0.1f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float t = (i * WaveSpacing) + (ii * BranchSpacing);
                    float z = t + (iii * BulletSpacing);
                    float s = BulletBaseSpeed + (i * BulletSpeedModifier);
                    Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(t);

                    var bullet = SpawnProjectile(1, z, pos);
                    bullet.StartCoroutine(bullet.LerpSpeed(s, s * 0.5f, 2f));
                    bullet.Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}