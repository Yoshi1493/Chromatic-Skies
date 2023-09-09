using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class GeminiBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 45;
    const float WaveSpacing = 0.3f;
    const int BranchCount = 2;
    const float SpawnOffset = 0.5f;

    protected override float ShootingCooldown => 0.02f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            float r = Random.Range(15f, 60f) * PositiveOrNegativeOne;

            for (int i = 1; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    Vector3 v = i * WaveSpacing * transform.up.RotateVectorBy(r);
                    float z = r + 90f;

                    Vector3 pos = v + (ii * 2 - 1) * SpawnOffset * Vector3.right.RotateVectorBy(r);

                    SpawnProjectile(0, z, pos).Fire();
                    SpawnProjectile(0, z, -pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(10f);
        }
    }
}