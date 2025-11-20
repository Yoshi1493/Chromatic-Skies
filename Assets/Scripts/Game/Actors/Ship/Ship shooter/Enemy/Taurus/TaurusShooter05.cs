using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusShooter05 : CommonEnemyShooter<Laser>
{
    const int WaveCount = 3;
    const float WaveSpacing = BranchSpacing / WaveCount;
    const int BranchCount = 8;
    const float BranchSpacing = 360f / BranchCount;
    public const float LaserSpawnOffset = 90f;
    public const float LaserSpawnRadius = 0.5f;

    protected override float ShootingCooldown => 1.2f;

    public event System.Action<float> ShootAction;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                float r = Random.Range(0f, BranchSpacing);
                ShootAction?.Invoke(r);

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = (ii * BranchSpacing) + r;
                    float t = z + LaserSpawnOffset;
                    Vector3 pos = LaserSpawnRadius * transform.up.RotateVectorBy(t);

                    SpawnProjectile(0, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(8f);
        }
    }
}