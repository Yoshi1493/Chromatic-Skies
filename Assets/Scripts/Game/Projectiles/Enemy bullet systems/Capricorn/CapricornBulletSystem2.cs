using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 80;
    const float WaveSpacing = 15f;
    const int BranchCount = 4;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 0.02f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            SetSubsystemEnabled(1);

            float r = Random.Range(0f, BranchSpacing);

            for (int i = 0; i < WaveCount; i++)
            {
                float t = i * WaveSpacing;

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = r + (ii * BranchSpacing);
                    Vector3 pos = i * 0.04f * transform.up.RotateVectorBy(t);

                    SpawnProjectile(0, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return ownerShip.MoveToRandomPosition(2f, 1f, 1.44f, 2f);
        }
    }
}