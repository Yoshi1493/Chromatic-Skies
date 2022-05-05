using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem21 : EnemyBulletSubsystem<EnemyBullet>
{
    const int BranchCount = 16;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 5f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return WaitForSeconds(1f);

            float randOffset = Random.Range(0f, BranchSpacing * 0.5f);

            for (int i = 0; i < BranchCount; i++)
            {
                float z = i * BranchSpacing + randOffset;
                SpawnProjectile(2, z, Vector3.zero).Fire();
            }

            yield return WaitForSeconds(4f);
        }
    }
}