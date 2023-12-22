using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletSpacing = 360f / BulletCount;
    const float SpinRadius = 1.2f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            SetSubsystemEnabled(1);

            float x = screenHalfWidth * 0.5f;
            float y = screenHalfHeight;

            for (int i = 0; enabled; i++)
            {
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

                        var bullet = SpawnProjectile(0, z, pos, false) as GeminiBullet30;
                        bullet.rotationAxis = v2 - v1;
                        bullet.rotationPoint = v1;
                        bullet.Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(3f);
        }
    }
}