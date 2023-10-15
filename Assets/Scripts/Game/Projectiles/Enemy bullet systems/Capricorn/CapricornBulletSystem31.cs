using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem31 : EnemyShooter<EnemyBullet>
{
    const float WaveCount = 3;
    const int BranchCount = 9;
    const float BranchSpacing = 120f / BranchCount;
    const int MaxBulletCount = 7;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedMultiplier = 0.2f;

    protected override float ShootingCooldown => 1.0f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            float r = PlayerPosition.GetRotationDifference(transform.position);
            bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

            for (int ii = 0; ii < BranchCount; ii++)
            {
                int bulletCount = Mathf.Min((int)Mathf.PingPong(ii, BranchCount / 2) + 1, MaxBulletCount);

                for (int iii = 0; iii < bulletCount; iii++)
                {
                    float z = ((ii - ((BranchCount - 1) / 2f)) * BranchSpacing) + r;
                    float s = BulletBaseSpeed + (iii * BulletSpeedMultiplier);
                    Vector3 pos = Vector3.zero;

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