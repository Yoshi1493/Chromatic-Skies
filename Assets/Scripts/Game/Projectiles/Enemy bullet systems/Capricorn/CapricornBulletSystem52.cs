using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem52 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 8;
    const float WaveSpacing = 360f / WaveCount / BranchCount;
    const int BranchCount = 3;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 8;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedMultiplier = 0.5f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        float r = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = (i * WaveSpacing) + (ii * BranchSpacing) + r;
                    float s = BulletBaseSpeed + (iii * BulletSpeedMultiplier);
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(iii / (BulletCount - 1f));
                    var bullet = SpawnProjectile(3, z, pos);
                    bullet.MoveSpeed = s;
                    bullet.Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}