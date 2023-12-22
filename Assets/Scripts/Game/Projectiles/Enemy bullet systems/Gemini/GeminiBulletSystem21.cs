using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const float WaveSpacing = BranchSpacing / 2f;
    const int BranchCount = 48;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const int BulletClumpCount = 3;
    const float BulletRotationSpeed = 15f;
    const float BulletRotationDuration = 2f;

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        while (enabled)
        {
            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    for (int iv = 0; iv < BulletCount; iv++)
                    {
                        int b = (iv % 2) + 2;
                        float z = (ii * WaveSpacing) + (iii * BranchSpacing);
                        Vector3 pos = Vector3.zero;

                        var bullet = SpawnProjectile(b, z, pos);
                        bullet.StartCoroutine(bullet.RotateBy((((ii % 2) + iii + iv) % BulletClumpCount - ((BulletClumpCount - 1) / 2f)) * BulletRotationSpeed, BulletRotationDuration, delay: 1f));
                        bullet.Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(3f);
        }
    }
}