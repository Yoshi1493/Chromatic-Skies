using System.Collections;
using UnityEngine;
using static CoroutineHelper;


public class SagittariusBulletSystem11 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = (360 - (int)SafeZone) / (int)WaveSpacing + 1;
    const float WaveSpacing = 8f;
    const float SafeZone = 40f;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletSpacing = 90f;
    const float BulletSpawnRadius = 0.5f;
    const float SpawnRadiusMultiplier = 0.5f;

    protected override float ShootingCooldown => 0.02f;

    protected override IEnumerator Shoot()
    {
        int r = 1;

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = ((SafeZone / 2f) + (i * WaveSpacing) + (ii * BranchSpacing)) * r;
                Vector3 pos = (BulletSpawnRadius + (ii * SpawnRadiusMultiplier)) * transform.up.RotateVectorBy(z);

                for (int iii = 0; iii < BulletCount; iii++)
                {
                    z += iii * BulletSpacing;

                    bulletData.colour = bulletData.gradient.Evaluate(iii);
                    SpawnProjectile(1, z, pos).Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        yield return WaitForSeconds(ShootingCooldown * 10f);

        r *= -1;

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = ((SafeZone / 2f) + (i * WaveSpacing) + (ii * BranchSpacing)) * r;
                Vector3 pos = (BulletSpawnRadius + (ii * SpawnRadiusMultiplier)) * transform.up.RotateVectorBy(z);

                for (int iii = 0; iii < BulletCount; iii++)
                {
                    z += iii * BulletSpacing;

                    bulletData.colour = bulletData.gradient.Evaluate(iii);
                    SpawnProjectile(1, z, pos).Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}