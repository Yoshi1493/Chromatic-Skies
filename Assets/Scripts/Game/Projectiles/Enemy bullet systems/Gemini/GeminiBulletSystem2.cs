using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class GeminiBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 45;
    const float WaveSpacing = 0.3f;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletSpacing = 360f / BulletCount;
    const float SpawnOffset = 0.5f;

    Queue<(Vector3 xy, float z)> bulletPosRotData = new(WaveCount * BranchCount * BulletCount);
    protected override float ShootingCooldown => 0.02f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {

            float r = Random.Range(30f, 75f) * PositiveOrNegativeOne;

            for (int i = 1; i < WaveCount; i++)
            {
                float z = (i * 2f) + 90f + r;
                Vector3 v1 = i * WaveSpacing * transform.up.RotateVectorBy(r);

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    Vector3 v2 = SpawnOffset * Vector3.right.RotateVectorBy((ii * BranchSpacing) + r) + v1;

                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        Vector3 pos = v2.RotateVectorBy(iii * BulletSpacing);
                        bulletPosRotData.Enqueue((pos, z));

                        SpawnProjectile(0, z, pos).Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);

            for (int i = 1; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        var xyz = bulletPosRotData.Dequeue();
                        float x = -xyz.xy.x;
                        float y = xyz.xy.y;
                        float z = -xyz.z;
                        Vector3 pos = new(x, y);

                        SpawnProjectile(1, z, pos).Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            bulletPosRotData.Clear();

            yield return WaitForSeconds(10f);
        }
    }
}