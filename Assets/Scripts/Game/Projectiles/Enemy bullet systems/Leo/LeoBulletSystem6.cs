using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem6 : EnemyShooter<EnemyBullet>
{
    const float WaveSpacing = 16f;
    const int BranchCount = 3;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpawnRadius = 12f;

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);
        SetSubsystemEnabled(2);

        float r = Random.Range(45f, 135f);

        for (int i = 0; enabled; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (i * WaveSpacing) + (ii * BranchSpacing) + r;
                Vector3 pos = BulletSpawnRadius * Vector3.up.RotateVectorBy(z);

                SpawnProjectile(0, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}