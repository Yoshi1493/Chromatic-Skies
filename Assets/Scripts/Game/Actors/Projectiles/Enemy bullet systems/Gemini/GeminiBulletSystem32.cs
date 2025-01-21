using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBulletSystem32 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 24;
    const float WaveSpacing = 720f / WaveCount;
    const int BranchCount = 2;
    const float BranchSpacing = (360f / BranchCount) - (WaveSpacing / 2f);
    const int BulletCount = 10;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpawnRadius = 0.4f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {                    
                    float t = iii * BulletSpacing;
                    Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(t);

                    float r = (i * WaveSpacing) + ((ii + 0.5f) * BranchSpacing);
                    Vector3 v1 = (BulletSpawnRadius * 10f) * Vector3.down.RotateVectorBy(r);

                    float z = v1.GetRotationDifference(pos);

                    bulletData.colour = bulletData.gradient.Evaluate(ii);

                    SpawnProjectile(2, z, pos).Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}