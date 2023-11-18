using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 200;
    const int BranchCount = 5;
    const float BranchSpacing = 360f / BranchCount;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        while (enabled)
        {
            StartMoveAction?.Invoke();

            float WaveSpacing = 0f;
            float r = Random.Range(0f, BranchSpacing);
            Vector3 pos = Vector3.zero;

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = (ii * BranchSpacing) + WaveSpacing + r;
                    SpawnProjectile(0, z, pos).Fire();
                }

                WaveSpacing += i;
                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(2f);
        }
    }
}