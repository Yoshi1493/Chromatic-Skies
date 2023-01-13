using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem2 : EnemyShooter<EnemyBullet>
{
    AnimationCurve spawnInterpolation = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    const int WaveCount = 39;
    const float WaveSpacing = 5f;
    const int BranchCount = 4;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 0.025f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        for (int i = 1; ; i *= -1)
        {
            SetSubsystemEnabled(1);

            yield return WaitForSeconds(1f);

            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    float z = i * ((ii * WaveSpacing) + (iii * BranchSpacing));
                    //Vector3 pos = i * Vector3.Lerp(Vector3.left, Vector3.right, spawnInterpolation.Evaluate((float)ii / WaveCount));
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(ii % 2, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
            yield return WaitForSeconds(3f);
        }
    }
}