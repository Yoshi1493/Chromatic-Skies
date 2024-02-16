using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem4 : EnemyShooter<Laser>
{
    const int WaveCount = 2;
    const int BranchCount = 6;
    const float BranchSpacing = 15f;
    const int LaserCount = 2;
    const float MaxAngleOffset = 45f;
    const float LaserSpawnRadius = 0.75f;

    protected override float ShootingCooldown => 2f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 1; ii <= BranchCount; ii++)
                {
                    float r = Random.Range(-MaxAngleOffset, MaxAngleOffset);
                    float t = Random.Range(-BranchSpacing, BranchSpacing);

                    for (int iii = 0; iii < LaserCount; iii++)
                    {
                        int d = iii % 2 * 2 - 1;
                        float z = d * (ii * BranchSpacing + t + 180f);
                        Vector3 pos = LaserSpawnRadius * -transform.up.RotateVectorBy(z + (d * r));

                        SpawnProjectile(i, z, pos).Fire(1f);
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(10f);
        }
    }
}