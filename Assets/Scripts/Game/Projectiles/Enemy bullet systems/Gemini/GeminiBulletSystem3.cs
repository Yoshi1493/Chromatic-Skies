using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 21;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    public const float BranchSpawnOffset = 5f;
    const int BulletCount = 2;
    const float BulletSpacing = 360f / BulletCount;
    const float SpinRadius = 1f;

    protected override float ShootingCooldown => 0.1f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            float y = PlayerPosition.y;

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float x = screenHalfWidth * Mathf.Sign(ii % 2 - 1);
                    Vector3 v1 = new(x, y, 0f);
                    Vector3 v2 = new(0f, y, 0f);

                    float z = v2.GetRotationDifference(v1);

                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float vx = SpinRadius * Mathf.Sin(iii * BulletSpacing * Mathf.Deg2Rad);
                        float vz = SpinRadius * Mathf.Cos(iii * BulletSpacing * Mathf.Deg2Rad);
                        Vector3 pos = v1 + new Vector3(vx, 0f, vz);
                        int b = (i + iii) % BulletCount;

                        var bullet = SpawnProjectile(b, z, pos, false) as GeminiBullet30;
                        bullet.rotationAxis = v2 - v1;
                        bullet.rotationPoint = v1;
                        bullet.Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(5f);
        }
    }
}