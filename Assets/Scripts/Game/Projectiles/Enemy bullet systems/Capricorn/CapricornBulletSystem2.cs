using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 45;
    const float WaveSpacing = 5f;
    const int BranchCount = 4;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 0.025f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        float i = 1;

        while (enabled)
        {
            SetSubsystemEnabled(1);

            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    float z = i * ((ii * WaveSpacing) + (iii * BranchSpacing));
                    Vector3 pos = i * Vector3.Lerp(Vector3.left, Vector3.right, (float)ii / WaveCount);

                    SpawnProjectile(ii % 2, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            i *= -1;
            yield return ownerShip.MoveToRandomPosition(1f, 1f, 1.44f, 1f);
        }
    }
}