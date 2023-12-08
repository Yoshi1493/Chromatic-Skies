using System.Collections;
using UnityEngine;

public class TaurusBulletSystem51 : EnemyShooter<Laser>
{
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int LaserCount = 4;
    const float LaserSpacing = 360f / LaserCount;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < BranchCount; i++)
        {
            float r = i * BranchSpacing;

            for (int ii = 0; ii < LaserCount; ii++)
            {
                float z = ii * LaserSpacing;
                Vector3 pos = PlayerPosition + transform.up.RotateVectorBy(z + r + 90f);

                SpawnProjectile(0, z, pos, false).Fire(1f);
            }
        }

        yield break;
    }
}