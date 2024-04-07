using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBulletSystem11 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 24;
    const float WaveSpacing = 720f / WaveCount;
    const int BranchCount = 2;
    const float BranchSpacing = (360f / BranchCount) - (WaveSpacing / 2f);
    const int BulletCount = 10;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpawnRadius = 0.5f;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 1f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = (i * WaveSpacing) + ((ii + 0.5f) * BranchSpacing);
                    float r = iii * BulletSpacing;
                    float s = BulletBaseSpeed + (ii * BulletSpeedModifier);
                    Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(r);

                    bulletData.colour = bulletData.gradient.Evaluate(ii);

                    var bullet = SpawnProjectile(1, z, pos);
                    bullet.MoveSpeed = s;
                    bullet.Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}