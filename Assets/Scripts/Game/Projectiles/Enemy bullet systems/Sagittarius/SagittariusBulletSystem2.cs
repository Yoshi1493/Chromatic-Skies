using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 119;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        for (int i = 1; enabled; i *= -1)
        {
            float waveSpacing = 0f;

            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    float z = i * (waveSpacing + (iii * BranchSpacing));
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(0, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
                waveSpacing += ii;
            }

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(2f);
        }

    }
}