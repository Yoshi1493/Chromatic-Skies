using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class LeoBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 60;
    const float WaveSpacing = 360f / WaveCount;
    const float MaxWaveSpacingOffset = 12f;
    const int BranchCount = 3;
    const float BranchSpacing = 180f / BranchCount;
    const float BulletSpawnRadius = 0.5f;
    const float BulletBaseSpeed = 3f;
    const float BulletSpeedModifier = -0.5f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        StartMoveAction?.Invoke();
        SetSubsystemEnabled(1);
        SetSubsystemEnabled(2);

        int d = PositiveOrNegativeOne;

        for (int i = 0; enabled; i++)
        {
            float r = Mathf.Clamp01((i - WaveCount) * 0.0005f) * Random.Range(-MaxWaveSpacingOffset, MaxWaveSpacingOffset);

            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = d * ((i * WaveSpacing) + (ii * BranchSpacing) + r);
                float s = BulletBaseSpeed + (ii * BulletSpeedModifier);
                Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(z);

                bulletData.colour = bulletData.gradient.Evaluate(ii / (BranchCount - 1f));

                var bullet = SpawnProjectile(0, z, pos);
                bullet.MoveSpeed = s;
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}