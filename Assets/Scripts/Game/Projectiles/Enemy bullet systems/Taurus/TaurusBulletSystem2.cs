using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const float WaveOffset = 10f;
    const int BranchCount = 4;
    const float BranchOffset = 360f / BranchCount;
    const int BulletCount = 16;

    List<EnemyBullet> bullets = new(BranchCount * BulletCount);

    protected override float ShootingCooldown => 0.02f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            float theta = transform.eulerAngles.z;

            for (int i = 0; i < BranchCount; i++)
            {
                for (int j = 0; j < BulletCount; j++)
                {
                    Vector3 v1 = Vector3.up.RotateVectorBy(i * BranchOffset + theta);
                    Vector3 v2 = Vector3.up.RotateVectorBy((i + 1) * BranchOffset + theta);
                    Vector3 spawnPos = 2f * Vector3.Lerp(v1, v2, (float)j / BulletCount);

                    float z = spawnPos.GetRotationDifference(Vector3.zero);

                    bullets.Add(SpawnProjectile(0, z, spawnPos));

                    yield return WaitForSeconds(ShootingCooldown);
                }
            }

            bullets.ForEach(b => b.Fire());
            bullets.Clear();

            transform.Rotate(0f, 0f, WaveOffset);
        }

    }
}