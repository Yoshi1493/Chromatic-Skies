using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBulletSystem31 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 8;
    const float WaveSpacing = 6;
    const int BranchCount = 3;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 3;
    const float BulletSpacing = 15f;
    const float BulletSpawnRadius = 1f;
    const float SpawnRadiusDecreaseRate = 0.1f;
    const float BulletBaseSpeed = 2.5f;
    const float BulletSpeedMultiplier = 0.5f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        float r = Random.Range(0f, BranchSpacing);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = -((i * WaveSpacing) + (ii * BranchSpacing) + ((iii - ((BulletCount - 1) / 2f)) * BulletSpacing) + r);
                    float s = BulletBaseSpeed + (iii * BulletSpeedMultiplier);
                    Vector3 pos = (BulletSpawnRadius - (i * SpawnRadiusDecreaseRate)) * transform.up.RotateVectorBy(z);

                    var bullet = SpawnProjectile(2, z, pos);
                    bullet.MoveSpeed = s;
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}