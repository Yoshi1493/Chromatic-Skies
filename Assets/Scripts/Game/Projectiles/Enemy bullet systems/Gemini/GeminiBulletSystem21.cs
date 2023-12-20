using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const float WaveSpacing = BranchSpacing / 2f;
    const int BranchCount = 30;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletRotationSpeed = 15f;
    const float BulletRotationDuration = 1f;

    protected override float ShootingCooldown => 0.5f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    int b = (iii % 2) + 2;
                    float z = (i * WaveSpacing) + (ii * BranchSpacing);
                    Vector3 pos = Vector3.zero;

                    var bullet = SpawnProjectile(b, z, pos);
                    bullet.StartCoroutine(bullet.RotateBy((iii % 2 * 2 - 1) * BulletRotationSpeed, BulletRotationDuration));
                    bullet.Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}