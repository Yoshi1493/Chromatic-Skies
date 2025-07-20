using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusShooter02 : EnemyShooter<EnemyBullet>
{
    const int RepeatCount = 2;
    const float RepeatSpacing = BranchSpacing / 2f;
    const int WaveCount = 3;
    const int BranchCount = 15;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        while (enabled)
        {
            for (int i = 0; i < RepeatCount; i++)
            {
                float r = PlayerPosition.GetRotationDifference(transform.position);

                for (int ii = 0; ii < WaveCount; ii++)
                {
                    for (int iii = 0; iii < BranchCount; iii++)
                    {
                        float z = (i * RepeatSpacing) + (iii * BranchSpacing) + r;
                        Vector3 pos = Vector3.zero;

                        bulletData.colour = bulletData.gradient.Evaluate(i);

                        SpawnProjectile(2, z, pos).Fire();
                    }

                    yield return WaitForSeconds(ShootingCooldown);
                }

                yield return WaitForSeconds(1f);
            }

            yield return WaitForSeconds(2f);
        }
    }
}