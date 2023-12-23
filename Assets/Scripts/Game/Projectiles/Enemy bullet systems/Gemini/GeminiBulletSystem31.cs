using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBulletSystem31 : EnemyShooter<EnemyBullet>
{
    const float WaveSpacing = 0.0025f;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletSpacing = 360f / BulletCount;
    const float SpinRadius = 1.2f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            for (int i = 0; enabled; i++)
            {
                float x = screenHalfWidth * (0.5f + Mathf.PingPong(i * WaveSpacing, 0.25f));
                float y = screenHalfHeight;

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float r = ii * BranchSpacing;
                    Vector3 v1 = new Vector3(x, y, 0f).RotateVectorBy(r);
                    Vector3 v2 = new Vector3(x, -y, 0f).RotateVectorBy(r);

                    float z = v2.GetRotationDifference(v1);

                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float vx = SpinRadius * Mathf.Sin((i + (iii * BulletSpacing)) * Mathf.Deg2Rad);
                        float vz = SpinRadius * Mathf.Cos((i + (iii * BulletSpacing)) * Mathf.Deg2Rad);
                        Vector3 pos = v1 + new Vector3(vx, 0f, vz);

                        bulletData.colour = bulletData.gradient.Evaluate((i + iii) % BulletCount);

                        var bullet = SpawnProjectile(1, z, pos, false) as GeminiBullet31;
                        bullet.rotationAxis = v2 - v1;
                        bullet.rotationPoint = v1;
                        bullet.Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}