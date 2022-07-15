using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 21;
    const float WaveSpacing = 0.5f;
    const int BranchCount = 3;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpacing = 5f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        while (enabled)
        {
            for (int i = 1; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = Random.Range(0f, 360f);
                    float t = (i * BulletSpacing) + (ii * BranchSpacing);
                    Vector3 pos = i * WaveSpacing * transform.up.RotateVectorBy(t);

                    SpawnProjectile(0, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return ownerShip.MoveToRandomPosition(1f, delay: 6f);
        }
    }
}