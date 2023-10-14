using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 40;
    const float WaveSpacing = 5f;
    const int BranchCount = 4;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 1 / 60f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();        

        for (int i = 1; enabled; i *= -1)
        {
            StartMoveAction?.Invoke();
            yield return WaitForSeconds(1f);

            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    int b = ii % 2;
                    float z = i * ((ii * WaveSpacing) + (iii * BranchSpacing));
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(b, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1.2f);

            SetSubsystemEnabled(1);
            yield return WaitForSeconds(3f);
        }
    }
}