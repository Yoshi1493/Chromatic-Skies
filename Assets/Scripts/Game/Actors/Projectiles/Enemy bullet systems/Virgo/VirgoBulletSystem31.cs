using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem31 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 2;
    const float WaveSpacing = 90f;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 36;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpawnRadius = 2f;
    const float BulletRotationSpeed = 60f;
    const float BulletRotationDuration = 5f;

    protected override float ShootingCooldown => 3f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    int b = iii % 2;
                    float t = (i * WaveSpacing) + (ii * BranchSpacing);
                    float z = iii * BulletSpacing;
                    Vector3 pos = BulletSpawnRadius * Vector3.right.RotateVectorBy(t);

                    bulletData.colour = bulletData.gradient.Evaluate(b);

                    var bullet = SpawnProjectile(1, z, pos);
                    bullet.Fire();
                    bullet.StartCoroutine(bullet.RotateBy((b * 2 - 1) * BulletRotationSpeed, BulletRotationDuration, delay: 1f));
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}