using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem11 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const int BranchCount = 8;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletRotationSpeed = 60f;

    protected override float ShootingCooldown => 0.5f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = ii * BranchSpacing;
                    float r = (iii % 2 * 2 - 1) * BulletRotationSpeed;
                    Vector3 pos = Vector3.zero;

                    var bullet = SpawnProjectile(1, z, pos);
                    bullet.StartCoroutine(bullet.RotateBy(r, 1f));
                    bullet.Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}