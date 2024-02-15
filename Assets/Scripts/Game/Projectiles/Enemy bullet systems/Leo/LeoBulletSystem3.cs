using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 60;
    const float WaveSpacing = 360f / WaveCount;
    const float MaxWaveSpacingOffset = 12f;
    const int BranchCount = 3;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpawnRadius = 0.5f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);
        SetSubsystemEnabled(2);

        for (int i = 0; enabled; i++)
        {
            float r = Mathf.Clamp01((i - WaveCount) * 0.002f) * Random.Range(-MaxWaveSpacingOffset, MaxWaveSpacingOffset);

            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (i * WaveSpacing) + (ii * BranchSpacing) + r;
                Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(z);

                SpawnProjectile(0, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }

}