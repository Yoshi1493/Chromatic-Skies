using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const float WaveSpacing = 10f;
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

        float i = 0f;

        while (enabled)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float t = ii * BranchSpacing;

                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float vx = SpinRadius * Mathf.Sin((i + (iii * BulletSpacing)) * Mathf.Deg2Rad) + BranchSpawnOffset;
                    float vy = screenHalfHeight;
                    float vz = SpinRadius * Mathf.Cos((i + (iii * BulletSpacing)) * Mathf.Deg2Rad);
                    Vector3 pos = new(vx, vy, vz);
                    MathHelper.RotateVectorBy(ref pos, t);
                    float z = t;

                    SpawnProjectile(0, z, pos, false).Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
            i += WaveSpacing;
        }
    }
}