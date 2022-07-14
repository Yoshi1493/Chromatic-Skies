using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 60;
    const float WaveSpacing = 12f;
    const int BranchCount = 4;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 0.04f;

    protected override IEnumerator Shoot()
    {
        float r = Random.Range(0f, BranchSpacing);

        for (int i = 0; i < WaveCount; i++)
        {
            float t = i * -WaveSpacing;

            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = r + t + (ii * BranchSpacing);
                Vector3 pos = transform.up.RotateVectorBy(t);

                SpawnProjectile(1, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}