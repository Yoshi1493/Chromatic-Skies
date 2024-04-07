using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int RepeatCount = 5;
    const int WaveCount = 5;
    const float WaveSpacing = 15f;
    const int BranchCount = 5;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SpawnProjectile(0, 0f, Vector3.zero).Fire();

        StartMoveAction?.Invoke();
        yield return WaitForSeconds(2f);

        while (enabled)
        {
            for (int i = 0; i < RepeatCount; i++)
            {
                for (int ii = 0; ii < WaveCount; ii++)
                {
                    for (int iii = 0; iii < BranchCount; iii++)
                    {
                        float z = (i % 2 * 2 - 1) * ((ii * WaveSpacing) + (iii * BranchSpacing));
                        Vector3 pos = Vector3.zero;

                        SpawnProjectile(2, z, pos).Fire();
                    }

                    yield return WaitForSeconds(ShootingCooldown);
                }

                yield return WaitForSeconds(0.25f);
            }

            yield return WaitForSeconds(1.5f);
        }
    }
}